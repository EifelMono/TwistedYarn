using System;
namespace EifelMono.TwistedYarn.Tests;

/*
public class ChildTests
{
    [Fact]
    public async void Test1()
    {
        using var childCancellationTokenSource = new CancellationTokenSource();

        using var cancellationTokenNode = new CancellationTokenNode()
            .AddChild(childCancellationTokenSource);

        Assert.False(cancellationTokenNode.HasTimeOut);
        Assert.False(cancellationTokenNode.IsCancellationRequested);
        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

        childCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
            Assert.True(false, "Something went wrong");
        }
        catch (Exception ex)
        {
            Assert.True(ex is OperationCanceledException);
            Assert.True(ex is TaskCanceledException);
        }

        Assert.True(cancellationTokenNode.IsCancellationRequested);
        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
        Assert.True(cancellationTokenNode.Childs.IsCancellationRequested);
    }

    [Fact]
    public async void Test2()
    {
        using var childCancellationTokenSource = new CancellationTokenSource();

        using var cancellationTokenNode = new CancellationTokenNode()
            .AddChild(childCancellationTokenSource.Token);

        Assert.False(cancellationTokenNode.HasTimeOut);
        Assert.False(cancellationTokenNode.IsCancellationRequested);
        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

        childCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
            Assert.True(false, "Something went wrong");
        }
        catch (Exception ex)
        {
            Assert.True(ex is OperationCanceledException);
            Assert.True(ex is TaskCanceledException);
        }

        Assert.True(cancellationTokenNode.IsCancellationRequested);
        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
        Assert.True(cancellationTokenNode.Childs.IsCancellationRequested);
    }

    [Fact]
    public async void Test3()
    {
        var childCancellationTokenSource = new CancellationTokenSource();

        using var cancellationTokenNode = new CancellationTokenNode()
            .AddChild(childCancellationTokenSource, disposable: true);

        Assert.False(cancellationTokenNode.HasTimeOut);
        Assert.False(cancellationTokenNode.IsCancellationRequested);
        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

        childCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
            Assert.True(false, "Something went wrong");
        }
        catch (Exception ex)
        {
            Assert.True(ex is OperationCanceledException);
            Assert.True(ex is TaskCanceledException);
        }

        Assert.True(cancellationTokenNode.IsCancellationRequested);
        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
        Assert.True(cancellationTokenNode.Childs.IsCancellationRequested);
    }
}

*/