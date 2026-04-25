namespace SlotEngine.Domain.ValueObjects;

public sealed class ReelSet
{
    public IReadOnlyList<ReelStrip> Strips { get; }

    public int ReelCount => Strips.Count;

    public ReelSet(IEnumerable<ReelStrip> strips)
    {
        ArgumentNullException.ThrowIfNull(strips);
        var list = strips.ToList();
        if (list.Count == 0)
            throw new ArgumentException("Reel set must contain at least one strip.", nameof(strips));
        if (list.Any(s => s is null))
            throw new ArgumentException("Reel set cannot contain null strips.", nameof(strips));
        Strips = list.AsReadOnly();
    }

    public ReelStrip this[int reelIndex] => Strips[reelIndex];
}
