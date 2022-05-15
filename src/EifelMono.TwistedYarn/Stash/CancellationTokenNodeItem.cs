#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
namespace EifelMono.TwistedYarn.Stash;

public abstract class CancellationTokenNodeItem : IDisposable
{
    public virtual object? Value { get; set; }

    public bool Disposeable { get; set; }

    public abstract void Dispose();

    public abstract bool IsCancellationRequested { get; }
}

public abstract class CancellationTokenNodeItem<T> : CancellationTokenNodeItem
{
    public new T Value { get => (T)base.Value!; set => base.Value = value; }
}

public class CancellationTokenNodeItemToken : CancellationTokenNodeItem<CancellationToken>
{
    public override void Dispose() { }

    public override bool IsCancellationRequested => Value.IsCancellationRequested;
}

public class CancellationTokenNodeItemSource : CancellationTokenNodeItem<CancellationTokenSource>
{
    public override void Dispose()
    {
        if (Disposeable)
            Value.Dispose();
    }

    public override bool IsCancellationRequested => Value.IsCancellationRequested;
}
