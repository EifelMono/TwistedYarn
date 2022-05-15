using System;
namespace EifelMono.TwistedYarn.Tests;

public class PlayTests
{
    [Fact]
    public async void Test1()
    {
        using var childCancellationTokenSource = new CancellationTokenSource();

        using var cancellationTokenNode = new CancellationTokenNode()
            .AddChild(childCancellationTokenSource);
        try
        {
            _ = cancellationTokenNode.Token;
        }
        catch(Exception ex)
        {
            if (cancellationTokenNode.IsCancellationRequested)
            {
                
            }
        }
    }

}

