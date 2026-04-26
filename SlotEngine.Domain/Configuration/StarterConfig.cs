using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Domain.Configuration;

public static class StarterConfig
{
    public const int WildSymbolId = 0;

    public const int ReelCount = 3;
    public const int RowCount = 3;
    public const int StripLength = 30;

    public static ReelSet BuildReelSet() => new(new[]
    {
        new ReelStrip(new[] { 1, 3, 2, 5, 1, 4, 2, 7, 3, 1, 6, 2, 4, 8, 5, 1, 9, 3, 2, 6, 4, 1, 5, 7, 3, 2, 8, 4, 6, 0 }),
        new ReelStrip(new[] { 2, 1, 4, 3, 6, 1, 2, 5, 8, 3, 1, 4, 2, 7, 5, 3, 1, 9, 6, 2, 4, 1, 8, 5, 3, 7, 2, 4, 6, 0 }),
        new ReelStrip(new[] { 3, 2, 1, 5, 4, 2, 1, 7, 6, 3, 1, 2, 5, 4, 8, 1, 2, 9, 3, 6, 1, 4, 5, 3, 2, 7, 4, 8, 6, 0 })
    });

    public static IReadOnlyList<Payline> BuildPaylines() => new[]
    {
        new Payline(new[] { 1, 1, 1 }),
        new Payline(new[] { 0, 0, 0 }),
        new Payline(new[] { 2, 2, 2 }),
        new Payline(new[] { 0, 2, 0 }),
        new Payline(new[] { 2, 0, 2 })
    };

    public static Paytable BuildPaytable() => new(new[]
    {
        new PaytableEntry(WildSymbolId, 3, 500),
        new PaytableEntry(1, 3, 5),
        new PaytableEntry(2, 3, 8),
        new PaytableEntry(3, 3, 10),
        new PaytableEntry(4, 3, 12),
        new PaytableEntry(5, 3, 20),
        new PaytableEntry(6, 3, 25),
        new PaytableEntry(7, 3, 50),
        new PaytableEntry(8, 3, 75),
        new PaytableEntry(9, 3, 200)
    });
}
