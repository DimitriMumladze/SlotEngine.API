using SlotEngine.Application.Abstractions;
using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Application.Slots;

public static class ReelSpinner
{
    public static int[] Spin(ReelSet reelSet, IRng rng)
    {
        ArgumentNullException.ThrowIfNull(reelSet);
        ArgumentNullException.ThrowIfNull(rng);

        var stops = new int[reelSet.ReelCount];
        for (var reel = 0; reel < reelSet.ReelCount; reel++)
            stops[reel] = rng.Next(reelSet[reel].Length);
        return stops;
    }
}
