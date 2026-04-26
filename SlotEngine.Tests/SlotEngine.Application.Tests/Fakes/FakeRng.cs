using SlotEngine.Application.Abstractions;

namespace SlotEngine.Application.Tests.Fakes;

public sealed class FakeRng : IRng
{
    private readonly Queue<int> _scripted;

    public FakeRng(params int[] values) => _scripted = new Queue<int>(values);

    public int Next(int maxExclusive)
    {
        if (_scripted.Count == 0)
            throw new InvalidOperationException("FakeRng exhausted: no more scripted values.");
        var v = _scripted.Dequeue();
        if (v < 0 || v >= maxExclusive)
            throw new InvalidOperationException($"FakeRng value {v} out of range [0,{maxExclusive}).");
        return v;
    }
}
