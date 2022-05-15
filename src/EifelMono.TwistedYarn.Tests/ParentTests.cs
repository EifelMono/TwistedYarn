using System;
namespace EifelMono.TwistedYarn.Tests;
public class ParentTests
{
    [Fact]
    public async void Test1()
    {
        using var parentCancellationTokenSource = new CancellationTokenSource();

        using var cancellationTokenNode = new CancellationTokenNode()
            .AddParent(parentCancellationTokenSource);

        Assert.False(cancellationTokenNode.Node.WithTimeOut);

        Assert.True(cancellationTokenNode.State.IsUndefined());
        Assert.False(cancellationTokenNode.IsCancellationRequested);
       
        parentCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(1));
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
        Assert.True(cancellationTokenNode.IsCanceled());

        Assert.True(cancellationTokenNode.IsParentCanceled());
        Assert.False(cancellationTokenNode.IsChildCanceled());
        Assert.False(cancellationTokenNode.IsNodeCanceled());
        Assert.False(cancellationTokenNode.IsNodeTimeOut());

        Assert.True(cancellationTokenNode.Parent.IsCancellationRequested);
        Assert.False(cancellationTokenNode.Child.IsCancellationRequested);
        Assert.False(cancellationTokenNode.Node.IsCancellationRequested);
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

