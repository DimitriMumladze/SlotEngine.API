using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Application.Slots;

public static class GridBuilder
{
    public static int[][] Build(ReelSet reelSet, IReadOnlyList<int> stops, int rowCount)
    {
        ArgumentNullException.ThrowIfNull(reelSet);
        ArgumentNullException.ThrowIfNull(stops);
        if (stops.Count != reelSet.ReelCount)
            throw new ArgumentException("Stops length must equal reel count.", nameof(stops));
        if (rowCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(rowCount), "Row count must be positive.");

        var grid = new int[reelSet.ReelCount][];
        for (var reel = 0; reel < reelSet.ReelCount; reel++)
        {
            var strip = reelSet[reel];
            var column = new int[rowCount];
            for (var row = 0; row < rowCount; row++)
                column[row] = strip.At(stops[reel] + row);
            grid[reel] = column;
        }
        return grid;
    }
}
