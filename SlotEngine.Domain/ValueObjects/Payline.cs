namespace SlotEngine.Domain.ValueObjects;

public sealed class Payline
{
    public IReadOnlyList<int> RowsPerReel { get; }

    public int Length => RowsPerReel.Count;

    public Payline(IEnumerable<int> rowsPerReel)
    {
        ArgumentNullException.ThrowIfNull(rowsPerReel);
        var list = rowsPerReel.ToList();
        if (list.Count == 0)
            throw new ArgumentException("Payline must cover at least one reel.", nameof(rowsPerReel));
        if (list.Any(r => r < 0))
            throw new ArgumentException("Payline row indices must be non-negative.", nameof(rowsPerReel));
        RowsPerReel = list.AsReadOnly();
    }

    public int RowAt(int reelIndex) => RowsPerReel[reelIndex];
}
