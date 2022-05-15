namespace EifelMono.TwistedYarn;

[Flags]
public enum CancellationTokenNodeState
{
    Undefined = 0b_0000_0000_0001,
    None = 0b_0000_0000_0010,

    Canceled = 0b_0000_0001_0000,

    ParentCanceled = 0b_0001_0000_0000,
    ChildCanceled = 0b_0010_0000_0000,

    NodeCanceled = 0b_0100_0000_0000,
    NodeTimeOut = 0b_1000_0000_0000,
}

public static class CancellationTokenNodeStateExtensions
{
    public static bool Is(this CancellationTokenNodeState thisValue, CancellationTokenNodeState value)
        => (thisValue & value) == value;

    public static bool IsUndefined(this CancellationTokenNodeState thisValue)
        => thisValue == CancellationTokenNodeState.Undefined;
    public static bool IsNone(this CancellationTokenNodeState thisValue)
        => thisValue == CancellationTokenNodeState.None;

    public static bool IsCanceled(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.Canceled);

    public static bool IsParentCanceled(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.ParentCanceled);
    public static bool IsChildCanceled(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.ChildCanceled);

    public static bool IsNodeCanceled(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.NodeCanceled);
    public static bool IsNodeTimeOut(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.NodeTimeOut);

    public static bool IsUndefined(this CancellationTokenNode thisValue)
        => thisValue.State.IsUndefined();
    public static bool IsNone(this CancellationTokenNode thisValue)
        => thisValue.State.IsNone();

    public static bool IsCanceled(this CancellationTokenNode thisValue)
        => thisValue.State.IsCanceled();

    public static bool IsParentCanceled(this CancellationTokenNode thisValue)
        => thisValue.State.IsParentCanceled();
    public static bool IsChildCanceled(this CancellationTokenNode thisValue)
        => thisValue.State.IsChildCanceled();

    public static bool IsNodeCanceled(this CancellationTokenNode thisValue)
        => thisValue.State.IsNodeCanceled();
    public static bool IsNodeTimeOut(this CancellationTokenNode thisValue)
        => thisValue.State.IsNodeTimeOut();
}
