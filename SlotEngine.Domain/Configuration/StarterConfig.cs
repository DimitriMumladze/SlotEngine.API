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
        new ReelStrip(new[] { 1, 2, 1, 3, 2, 1, 4, 2, 5, 1, 3, 2, 1, 4, 8, 2, 1, 6, 3, 9, 2, 4, 3, 1, 5, 7, 2, 4, 3, 0 }),
        new ReelStrip(new[] { 2, 1, 3, 1, 4, 2, 1, 3, 2, 5, 1, 4, 2, 1, 8, 3, 2, 9, 1, 4, 6, 2, 3, 1, 5, 4, 2, 7, 3, 0 }),
        new ReelStrip(new[] { 3, 1, 2, 5, 1, 4, 2, 3, 1, 6, 2, 4, 1, 7, 2, 3, 1, 9, 4, 2, 1, 5, 3, 8, 2, 1, 4, 3, 2, 0 })
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
        new PaytableEntry(WildSymbolId, 3, 600),
        new PaytableEntry(1, 3, 5),
        new PaytableEntry(2, 3, 8),
        new PaytableEntry(3, 3, 18),
        new PaytableEntry(4, 3, 30),
        new PaytableEntry(5, 3, 80),
        new PaytableEntry(6, 3, 200),
        new PaytableEntry(7, 3, 250),
        new PaytableEntry(8, 3, 350),
        new PaytableEntry(9, 3, 450)
    });
}
