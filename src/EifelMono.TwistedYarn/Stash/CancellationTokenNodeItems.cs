#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
namespace EifelMono.TwistedYarn.Stash;

public abstract class CancellationTokenNodeItems<T> : IDisposable where T : CancellationTokenNodeItem
{
    public void Dispose()
        => Items.ForEach(item => item.Dispose());

    internal List<T> Items { get; set; } = new List<T>();

    internal abstract IEnumerable<CancellationToken> Tokens { get; }

    public bool IsCancellationRequested => Items.Any(item => item.IsCancellationRequested);
}

public class CancellationTokenNodeTokens : CancellationTokenNodeItems<CancellationTokenNodeItemToken>
{
    public void Add(CancellationToken cancellationToken)
    {
        Items.Add(new CancellationTokenNodeItemToken
        {
            Value = cancellationToken
        });
    }
    internal override IEnumerable<CancellationToken> Tokens => Items.Select(i => i.Value);
}

public class CancellationTokenNodeSources : CancellationTokenNodeItems<CancellationTokenNodeItemSource>
{
    public void Add(CancellationTokenSource cancellationTokenSource, bool disposable = false)
    {
        Items.Add(new CancellationTokenNodeItemSource
        {
            Value = cancellationTokenSource,
            Disposeable = disposable
        });
    }
    internal override IEnumerable<CancellationToken> Tokens => Items.Select(i => i.Value.Token);
}
