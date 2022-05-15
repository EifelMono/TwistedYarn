using System;
namespace EifelMono.TwistedYarn.Tests;
public class ParentTests
{
    [Fact]
    public async void Test1()
    {
        using var parentCancellationTokenSource = new CancellationTokenSource();
        using var childCancellationTokenSource = new CancellationTokenSource();


        using var cancellationTokenNode = new CancellationTokenNode()
            .AddParent(parentCancellationTokenSource)
            .AddChild(childCancellationTokenSource);

        Assert.False(cancellationTokenNode.Node.WithTimeOut);

        Assert.True(cancellationTokenNode.CurrentState.IsUndefined());
        Assert.False(cancellationTokenNode.Token.IsCancellationRequested);

        Assert.True(cancellationTokenNode.CancellationRequestedTimeStamp== DateTime.MinValue);

        parentCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
        childCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(2));
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationTokenNode.Token);
            Assert.True(false, "Something went wrong");
        }
        catch (Exception ex)
        {
            Assert.True(ex is OperationCanceledException);
            Assert.True(ex is TaskCanceledException);
        }

        Assert.True(cancellationTokenNode.Token.IsCancellationRequested);
        Assert.True(cancellationTokenNode.Parent.IsCancellationRequested);
        Assert.False(cancellationTokenNode.Child.IsCancellationRequested);
        Assert.False(cancellationTokenNode.Node.IsCancellationRequested);

        Assert.True(cancellationTokenNode.CurrentState.IsCancellationRequested());

        Assert.False(cancellationTokenNode.CurrentState.IsNodeCancellationRequested());
        Assert.False(cancellationTokenNode.CurrentState.IsNodeTimeOutRequested());

        Assert.True(cancellationTokenNode.CurrentState.IsParentCancelRequested());
        Assert.False(cancellationTokenNode.CurrentState.IsChildCancellationRequested());


        Assert.True(cancellationTokenNode.CancellationRequestedState.IsCancellationRequested());

        Assert.False(cancellationTokenNode.CancellationRequestedState.IsNodeCancellationRequested());
        Assert.False(cancellationTokenNode.CancellationRequestedState.IsNodeTimeOutRequested());

        Assert.True(cancellationTokenNode.CancellationRequestedState.IsParentCancelRequested());
        Assert.False(cancellationTokenNode.CancellationRequestedState.IsChildCancellationRequested());

        Assert.True((DateTime.Now - cancellationTokenNode.CancellationRequestedTimeStamp).TotalSeconds < 1);

        await Task.Delay(TimeSpan.FromSeconds(1));


        Assert.True(cancellationTokenNode.CurrentState.IsCancellationRequested());

        Assert.False(cancellationTokenNode.CurrentState.IsNodeCancellationRequested());
        Assert.False(cancellationTokenNode.CurrentState.IsNodeTimeOutRequested());

        Assert.True(cancellationTokenNode.CurrentState.IsParentCancelRequested());
        Assert.True(cancellationTokenNode.CurrentState.IsChildCancellationRequested());


        Assert.True(cancellationTokenNode.CancellationRequestedState.IsCancellationRequested());

        Assert.False(cancellationTokenNode.CancellationRequestedState.IsNodeCancellationRequested());
        Assert.False(cancellationTokenNode.CancellationRequestedState.IsNodeTimeOutRequested());

        Assert.True(cancellationTokenNode.CancellationRequestedState.IsParentCancelRequested());
        Assert.False(cancellationTokenNode.CancellationRequestedState.IsChildCancellationRequested());

        Assert.True((DateTime.Now - cancellationTokenNode.CancellationRequestedTimeStamp).TotalSeconds > 1);
    }

    //[Fact]
    //public async void Test2()
    //{
    //    using var parentCancellationTokenSource = new CancellationTokenSource();

    //    using var cancellationTokenNode = new CancellationTokenNode()
    //        .AddParent(parentCancellationTokenSource.Token);

    //    Assert.False(cancellationTokenNode.HasTimeOut);
    //    Assert.False(cancellationTokenNode.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //    Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

    //    parentCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
    //    try
    //    {
    //        await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
    //        Assert.True(false, "Something went wrong");
    //    }
    //    catch (Exception ex)
    //    {
    //        Assert.True(ex is OperationCanceledException);
    //        Assert.True(ex is TaskCanceledException);
    //    }

    //    Assert.True(cancellationTokenNode.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //    Assert.True(cancellationTokenNode.Parents.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);
    //}

    //[Fact]
    //public async void Test3()
    //{
    //    var parentCancellationTokenSource = new CancellationTokenSource();

    //    using var cancellationTokenNode = new CancellationTokenNode()
    //        .AddParent(parentCancellationTokenSource, disposable: true);

    //    Assert.False(cancellationTokenNode.HasTimeOut);
    //    Assert.False(cancellationTokenNode.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //    Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

    //    parentCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
    //    try
    //    {
    //        await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
    //        Assert.True(false, "Something went wrong");
    //    }
    //    catch (Exception ex)
    //    {
    //        Assert.True(ex is OperationCanceledException);
    //        Assert.True(ex is TaskCanceledException);
    //    }

    //    Assert.True(cancellationTokenNode.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //    Assert.True(cancellationTokenNode.Parents.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);
    //}

    //[Fact]
    //public async void Test4()
    //{
    //    var parentCancellationTokenSource = new CancellationTokenSource();

    //    {
    //        using var cancellationTokenNode = new CancellationTokenNode()
    //            .AddParent(parentCancellationTokenSource, disposable: true);

    //        Assert.False(cancellationTokenNode.HasTimeOut);
    //        Assert.False(cancellationTokenNode.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

    //        parentCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
    //        try
    //        {
    //            await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
    //            Assert.True(false, "Something went wrong");
    //        }
    //        catch (Exception ex)
    //        {
    //            Assert.True(ex is OperationCanceledException);
    //            Assert.True(ex is TaskCanceledException);
    //        }

    //        Assert.True(cancellationTokenNode.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //        Assert.True(cancellationTokenNode.Parents.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);
    //    }
    //    Assert.True(parentCancellationTokenSource.IsDisposed());
    //}

    //[Fact]
    //public async void Test5()
    //{
    //    var parentCancellationTokenSource = new CancellationTokenSource();

    //    {
    //        using var cancellationTokenNode = new CancellationTokenNode()
    //            .AddParent(parentCancellationTokenSource, disposable: false);

    //        Assert.False(cancellationTokenNode.HasTimeOut);
    //        Assert.False(cancellationTokenNode.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //        Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

    //        parentCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
    //        try
    //        {
    //            await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
    //            Assert.True(false, "Something went wrong");
    //        }
    //        catch (Exception ex)
    //        {
    //            Assert.True(ex is OperationCanceledException);
    //            Assert.True(ex is TaskCanceledException);
    //        }

    //        Assert.True(cancellationTokenNode.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //        Assert.True(cancellationTokenNode.Parents.IsCancellationRequested);
    //        Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);
    //    }
    //    Assert.False(parentCancellationTokenSource.IsDisposed());
    //    parentCancellationTokenSource.Dispose();
    //}

    //[Fact]
    //public async void Test6()
    //{
    //    using var parentCancellationTokenSource = new CancellationTokenSource();

    //    using var cancellationTokenNode = new CancellationTokenNode(parentCancellationTokenSource);

    //    Assert.False(cancellationTokenNode.HasTimeOut);
    //    Assert.False(cancellationTokenNode.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //    Assert.False(cancellationTokenNode.Parents.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);

    //    parentCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
    //    try
    //    {
    //        await Task.Delay(TimeSpan.FromSeconds(2), cancellationTokenNode.Token);
    //        Assert.True(false, "Something went wrong");
    //    }
    //    catch (Exception ex)
    //    {
    //        Assert.True(ex is OperationCanceledException);
    //        Assert.True(ex is TaskCanceledException);
    //    }

    //    Assert.True(cancellationTokenNode.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.IsCancellationRequestedByTimeOut);

    //    Assert.True(cancellationTokenNode.Parents.IsCancellationRequested);
    //    Assert.False(cancellationTokenNode.Childs.IsCancellationRequested);
    //}
}

