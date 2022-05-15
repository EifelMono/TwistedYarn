namespace EifelMono.TwistedYarn;

[Flags]
public enum CancellationTokenNodeState
{
    Undefined = 0b_0000_0000_0001,
    None = 0b_0000_0000_0010,

    CancellationRequested = 0b_0000_0001_0000,

    NodeCancellationRequested = 0b_0001_0000_0000,
    NodeTimeOutRequested = 0b_0010_0000_0000,

    ParentCancellationRequested = 0b_0100_0000_0000,
    ChildCancellationRequested = 0b_1000_0000_0000,
}

public static class CancellationTokenNodeStateExtensions
{
    public static bool Is(this CancellationTokenNodeState thisValue, CancellationTokenNodeState value)
        => (thisValue & value) == value;

    public static bool IsUndefined(this CancellationTokenNodeState thisValue)
        => thisValue == CancellationTokenNodeState.Undefined;
    public static bool IsNone(this CancellationTokenNodeState thisValue)
        => thisValue == CancellationTokenNodeState.None;

    public static bool IsCancellationRequested(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.CancellationRequested);

    public static bool IsNodeCancellationRequested(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.NodeCancellationRequested);
    public static bool IsNodeTimeOutRequested(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.NodeTimeOutRequested);
    public static bool IsParentCancelRequested(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.ParentCancellationRequested);
    public static bool IsChildCancellationRequested(this CancellationTokenNodeState thisValue)
        => thisValue.Is(CancellationTokenNodeState.ChildCancellationRequested);
}
