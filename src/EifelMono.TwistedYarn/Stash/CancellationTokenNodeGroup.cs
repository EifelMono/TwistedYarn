#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
namespace EifelMono.TwistedYarn.Stash;

public abstract class CancellationTokenNodeGroup : IDisposable
{
    public abstract void Dispose();

    public abstract IEnumerable<CancellationToken> ConcatedTokens { get; }

    public bool IsCancellationRequested => ConcatedTokens.Any(t => t.IsCancellationRequested);
}

public class CancellationTokenNodeGroupSource : CancellationTokenNodeGroup
{
    public CancellationTokenNodeSources Sources { get; } = new();

    public override void Dispose()
    {
        Sources.Dispose();
    }

    public override IEnumerable<CancellationToken> ConcatedTokens
        => Sources.Tokens;
}

public class CancellationTokenNodeGroupTokenSource : CancellationTokenNodeGroupSource
{
    public CancellationTokenNodeTokens Tokens { get; } = new();


    public override void Dispose()
    {
        base.Dispose();
        Tokens.Dispose();
    }

    public override IEnumerable<CancellationToken> ConcatedTokens
        => base.ConcatedTokens.Concat(Tokens.Tokens);
}

public class CancellationTokenNodeGroupNode : CancellationTokenNodeGroupSource
{
    public CancellationTokenSource? CancellationTokenSource { get; private set; }
    public bool IsCanceled => CancellationTokenSource?.IsCancellationRequested ?? false;

    public TimeSpan TimeOut { get; internal set; } = TimeSpan.Zero;
    public bool WithTimeOut => TimeOut != TimeSpan.Zero;
    public CancellationTokenSource? CancellationTokenSourceTimeOut { get; private set; }
    public bool IsTimeout => CancellationTokenSourceTimeOut?.IsCancellationRequested ?? false;

    internal void Create()
    {
        CancellationTokenSource = new CancellationTokenSource();
        Sources.Add(CancellationTokenSource, disposable: true);

        if (WithTimeOut)
        {
            CancellationTokenSourceTimeOut = new CancellationTokenSource();
            CancellationTokenSourceTimeOut.CancelAfter(TimeOut);
            Sources.Add(CancellationTokenSourceTimeOut, disposable: true);
        }
    }
}
