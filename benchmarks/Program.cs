using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EifelMono.TwistedYarn;

BenchmarkRunner.Run<LinkedBennchmark>();

[MemoryDiagnoser]
public class LinkedBennchmark
{
    [Benchmark]
    public void NormalLink()
    {
        using var parent = new CancellationTokenSource();
        using var child = new CancellationTokenSource();

        using var nodeSource = new CancellationTokenSource();
        using var nodeTimeOut = new CancellationTokenSource();
        nodeTimeOut.CancelAfter(TimeSpan.FromSeconds(1));

        using var node = CancellationTokenSource.CreateLinkedTokenSource(nodeSource.Token, nodeTimeOut.Token, parent.Token, child.Token);

        _ = node.Token;
    }

    [Benchmark]
    public void CancellationTokenNodeLink()
    {
        using var parent = new CancellationTokenSource();
        using var child = new CancellationTokenSource();

        using var node = new CancellationTokenNode()
            .AddChild(parent)
            .AddChild(child)
            .WithTimeOut(TimeSpan.FromSeconds(1));
        _ = node.Token;
    }

    [Benchmark]
    public void CancellationTokenNodeLinkSelfDisposing()
    {
        var parent = new CancellationTokenSource();
        var child = new CancellationTokenSource();

        using var node = new CancellationTokenNode()
            .AddChild(parent, disposable: true)
            .AddChild(child, disposable: true)
            .WithTimeOut(TimeSpan.FromSeconds(1));
        _ = node.Token;
    }

    [Benchmark]
    public void CancellationTokenShortNormalLink()
    {
        using var parent = new CancellationTokenSource();

        using var nodeSource = new CancellationTokenSource();
        using var nodeTimeOut = new CancellationTokenSource();
        nodeTimeOut.CancelAfter(TimeSpan.FromSeconds(1));

        using var node = CancellationTokenSource.CreateLinkedTokenSource(nodeSource.Token, nodeTimeOut.Token, parent.Token);

        _ = node.Token;
    }

    [Benchmark]
    public void CancellationTokenShortNodeLink()
    {
        using var parent = new CancellationTokenSource();

        using var node = new CancellationTokenNode(parent.Token)
            .AddChild(parent, disposable: true)
            .WithTimeOut(TimeSpan.FromSeconds(1));
        _ = node.Token;
    }
}
