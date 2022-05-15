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
        if (_node is { })
            _node.Dispose();
        Parent.Dispose();
        Child.Dispose();
        Node.Dispose();
    }

    #region All elements
    public CancellationTokenNodeGroupTokenSource Parent { get; set; } = new();
    public CancellationTokenNodeGroupTokenSource Child { get; set; } = new();
    public CancellationTokenNodeGroupNode Node { get; set; } = new();

    internal IEnumerable<CancellationToken> ConcatedTokens
        => Parent.ConcatedTokens.Concat(Child.ConcatedTokens).Concat(Node.ConcatedTokens);
    #endregion

    #region Node
    public void Cancel()
        => Node.CancellationTokenSource?.Cancel();

    protected CancellationTokenSource? _node;
    public CancellationToken Token => (_node ??= CreateNode()).Token;
    protected CancellationTokenSource CreateNode()
    {
        Node.Create();
        return CancellationTokenSource.CreateLinkedTokenSource(ConcatedTokens.ToArray());
    }

    public bool IsCancellationRequested
        => _node?.IsCancellationRequested ?? false;

    public CancellationTokenNodeState State
    {
        get
        {
            var result = CancellationTokenNodeState.Undefined;
            if (_node is null)
                return result;
            if (IsCancellationRequested)
            {
                result = CancellationTokenNodeState.Canceled;
                if (Node.CancellationTokenSource?.IsCancellationRequested ?? false)
                    result |= CancellationTokenNodeState.NodeCanceled;
                if (Node.CancellationTokenSourceTimeOut?.IsCancellationRequested ?? false)
                    result |= CancellationTokenNodeState.NodeTimeOut;
                if (Parent.IsCancellationRequested)
                    result |= CancellationTokenNodeState.ParentCanceled;
                if (Child.IsCancellationRequested)
                    result |= CancellationTokenNodeState.ChildCanceled;
            }
            else
                result = CancellationTokenNodeState.None;

            return result;
        }
    }
    #endregion
}
