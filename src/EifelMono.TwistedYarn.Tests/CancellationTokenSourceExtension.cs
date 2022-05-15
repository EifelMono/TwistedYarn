namespace EifelMono.TwistedYarn.Tests;

public static class CancellationTokenSourceExtension
{
    public static bool IsDisposed(this CancellationTokenSource thisValue)
    {
        try
        {
            _ = thisValue.Token;
            return false;
        }
        catch
        {
            return true;
        }
    }
}

