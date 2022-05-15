namespace EifelMono.TwistedYarn.Tests;

public class TimeoutTests
{
    //    [Fact]
    //    public async void Test1()
    //    {
    //        var cancellationTokenNode = new CancellationTokenNode()
    //            .WithTimeOut(TimeSpan.FromSeconds(1));

    //        Assert.True(cancellationTokenNode.Node.HasTimeOut);
    //        Assert.False(cancellationTokenNode.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

    //        try
    //        {
    //            await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
    //            Assert.True(false, "Something went wrong");
    //        }
    //        catch(Exception ex)
    //        {
    //            Assert.True(ex is OperationCanceledException);
    //            Assert.True(ex is TaskCanceledException);
    //        }

    //        Assert.True(cancellationTokenNode.IsCancellationRequested);
    //        Assert.True(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);
    //    }
}
