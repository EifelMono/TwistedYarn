#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
namespace EifelMono.TwistedYarn;

// -------------------------------------------------------------------------
// +--+- Parents
// |     +- IsCancellationRequested
// |     +- Tokens
// |        +- IsCancellationRequested
// |     +- Sources
// |        +- IsCancellationRequested
// | 
// |  +--+- Childs
// |  |     +- IsCancellationRequested
// |  |     +- Tokens
// |  |        +- IsCancellationRequested
// |  |     +- Sources
// |  |        +- IsCancellationRequested
// |  |  
// |  |  +--+- Nodes
// |  |  |     +- IsCancellationRequested
// |  |  |     +- CancellationTokenSourceSource
// |  |  |     +- CancellationTokenSourceTimeOut
// |  |  |
// +--+--+--+- Node (Source => linked from Parents, Childs, Nodes)
//             |
//             +- Cancel (CancellationTokenSourceNodeSource)
//             +- IsCancellationRequested (Node (Source => linked from Parents, Childs, Nodes)) 
//             +- IsCancellationRequestedByNode (CancellationTokenSourceNodeSource)
//             +- IsCancellationRequestedByTimeout (CancellationTokenSourceNodeTimeout)
//             +- HasTimeOut
//             +- IsTimeOut => IsCancellationRequestedByTimeout 
// -------------------------------------------------------------------------
public class CancellationTokenNode : IDisposable
{
    public CancellationTokenNode() { }

    public CancellationTokenNode(CancellationToken parentCancellationToken)
        => this.AddParent(parentCancellationToken);

    public CancellationTokenNode(CancellationTokenSource parentCancellationTokenSource, bool disposable = false)
        => this.AddParent(parentCancellationTokenSource, disposable);

    public void Dispose()
    {
        _nodeCancellationTokenRegistration?.Dispose();
        _node?.Dispose();
        Node.Dispose();
        Parent.Dispose();
        Child.Dispose();
    }

    #region All elements
    public CancellationTokenNodeGroupNode Node { get; set; } = new();
    public CancellationTokenNodeGroupTokenSource Parent { get; set; } = new();
    public CancellationTokenNodeGroupTokenSource Child { get; set; } = new();

    private CancellationTokenRegistration? _nodeCancellationTokenRegistration = null;

    public DateTime CancellationRequestedTimeStamp { get; private set; } = DateTime.MinValue;

    internal IEnumerable<CancellationToken> ConcatedTokens
        => Node.ConcatedTokens.Concat(Parent.ConcatedTokens).Concat(Child.ConcatedTokens);
    #endregion

    #region Node
    public void Cancel()
        => Node.CancellationTokenSource?.Cancel();

    protected CancellationTokenSource? _node;
    public CancellationToken Token => (_node ??= CreateNode()).Token;
    protected CancellationTokenSource CreateNode()
    {
        Node.Create();
        var node = CancellationTokenSource.CreateLinkedTokenSource(ConcatedTokens.ToArray());

        CancellationRequestedState = CancellationTokenNodeState.None;
        _nodeCancellationTokenRegistration = node.Token.Register(() =>
         {
             lock (_cancellationRequestedStateLockObject)
             {
                 if (_cancellationRequestedState == CancellationTokenNodeState.None)
                 {
                     _cancellationRequestedState = CurrentState;
                     CancellationRequestedTimeStamp = DateTime.Now;
                 }
             }
         });
        return node;
    }
    #endregion

    #region State
    public CancellationTokenNodeState CurrentState
    {
        get
        {
            var result = CancellationTokenNodeState.Undefined;
            if (_node is null)
                return result;
            if (_node.IsCancellationRequested)
            {
                result = CancellationTokenNodeState.CancellationRequested;
                if (Node.CancellationTokenSource?.IsCancellationRequested ?? false)
                    result |= CancellationTokenNodeState.NodeCancellationRequested;
                if (Node.CancellationTokenSourceTimeOut?.IsCancellationRequested ?? false)
                    result |= CancellationTokenNodeState.NodeTimeOutRequested;
                if (Parent.IsCancellationRequested)
                    result |= CancellationTokenNodeState.ParentCancellationRequested;
                if (Child.IsCancellationRequested)
                    result |= CancellationTokenNodeState.ChildCancellationRequested;
            }
            else
                result = CancellationTokenNodeState.None;

            return result;
        }
    }

    private CancellationTokenNodeState _cancellationRequestedState = CancellationTokenNodeState.Undefined;

    private readonly object _cancellationRequestedStateLockObject = new();
    public CancellationTokenNodeState CancellationRequestedState
    {
        get
        {
            lock (_cancellationRequestedStateLockObject)
            {
                if (_cancellationRequestedState.IsUndefined())
                    if (_node != null)
                        _cancellationRequestedState = CancellationTokenNodeState.None;
                return _cancellationRequestedState;
            }
        }
        set
        {
            lock (_cancellationRequestedStateLockObject)
                _cancellationRequestedState = value;
        }
    }
    #endregion
}
