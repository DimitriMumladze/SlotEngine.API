namespace SlotEngine.Domain.ValueObjects;

public sealed class ReelStrip
{
    public IReadOnlyList<int> Symbols { get; }

    public int Length => Symbols.Count;

    public ReelStrip(IEnumerable<int> symbols)
    {
        ArgumentNullException.ThrowIfNull(symbols);
        var list = symbols.ToList();
        if (list.Count == 0)
            throw new ArgumentException("Reel strip must contain at least one symbol.", nameof(symbols));
        Symbols = list.AsReadOnly();
    }

    public int At(int index)
    {
        var len = Length;
        var wrapped = ((index % len) + len) % len;
        return Symbols[wrapped];
    }
}
