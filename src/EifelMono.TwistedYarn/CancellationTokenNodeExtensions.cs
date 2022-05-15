namespace EifelMono.TwistedYarn;

public static class CancellationTokenNodeExtensions
{
    #region Parent
    public static CancellationTokenNode AddParent(this CancellationTokenNode thisValue, CancellationToken cancellationToken)
    {
        thisValue.Parent.Tokens.Add(cancellationToken);
        return thisValue;
    }

    public static CancellationTokenNode AddParent(this CancellationTokenNode thisValue, CancellationTokenSource cancellationTokenSource, bool disposable = false)
    {
        thisValue.Parent.Sources.Add(cancellationTokenSource, disposable);
        return thisValue;
    }
    #endregion

    #region Child
    public static CancellationTokenNode AddChild(this CancellationTokenNode thisValue, CancellationToken cancellationToken)
    {
        thisValue.Child.Tokens.Add(cancellationToken);
        return thisValue;
    }

    public static CancellationTokenNode AddChild(this CancellationTokenNode thisValue, CancellationTokenSource cancellationTokenSource, bool disposable = false)
    {
        thisValue.Child.Sources.Add(cancellationTokenSource, disposable);
        return thisValue;
    }

    public static CancellationTokenNode WithTimeOut(this CancellationTokenNode thisValue, TimeSpan timeOut)
    {
        thisValue.Node.TimeOut = timeOut;
        return thisValue;
    }
    #endregion
}
