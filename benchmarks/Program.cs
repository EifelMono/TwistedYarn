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

        using var nodeTimeOut = new CancellationTokenSource();
        nodeTimeOut.CancelAfter(TimeSpan.FromSeconds(1));

        using var node = CancellationTokenSource.CreateLinkedTokenSource(parent.Token, child.Token, nodeTimeOut.Token);

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
}
