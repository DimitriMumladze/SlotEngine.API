namespace SlotEngine.Domain.ValueObjects;

public readonly record struct PaytableEntry
{
    public int SymbolId { get; }
    public int MatchCount { get; }
    public int Multiplier { get; }

    public PaytableEntry(int symbolId, int matchCount, int multiplier)
    {
        if (matchCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(matchCount), "Match count must be positive.");
        if (multiplier < 0)
            throw new ArgumentOutOfRangeException(nameof(multiplier), "Multiplier cannot be negative.");

        SymbolId = symbolId;
        MatchCount = matchCount;
        Multiplier = multiplier;
    }
}

public sealed class Paytable
{
    private readonly IReadOnlyDictionary<int, IReadOnlyDictionary<int, int>> _table;

    public Paytable(IEnumerable<PaytableEntry> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);
        _table = entries
            .GroupBy(e => e.SymbolId)
            .ToDictionary(
                g => g.Key,
                g => (IReadOnlyDictionary<int, int>)g.ToDictionary(e => e.MatchCount, e => e.Multiplier));
    }

    public int Multiplier(int symbolId, int matchCount)
    {
        if (_table.TryGetValue(symbolId, out var byCount) && byCount.TryGetValue(matchCount, out var mult))
            return mult;
        return 0;
    }
}
