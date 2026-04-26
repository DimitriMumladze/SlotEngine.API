using SlotEngine.Application.Slots;
using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Application.Tests.Slots;

public class GridBuilderTests
{
    [Fact]
    public void Build_ReturnsThreeRowWindowStartingAtStop()
    {
        var reelSet = new ReelSet(new[]
        {
            new ReelStrip(new[] { 1, 2, 3, 4, 5 }),
            new ReelStrip(new[] { 6, 7, 8, 9, 10 }),
            new ReelStrip(new[] { 11, 12, 13, 14, 15 })
        });

        var grid = GridBuilder.Build(reelSet, new[] { 0, 1, 2 }, 3);

        Assert.Equal(new[] { 1, 2, 3 }, grid[0]);
        Assert.Equal(new[] { 7, 8, 9 }, grid[1]);
        Assert.Equal(new[] { 13, 14, 15 }, grid[2]);
    }

    [Fact]
    public void Build_WrapsAroundEndOfStrip()
    {
        var reelSet = new ReelSet(new[]
        {
            new ReelStrip(new[] { 1, 2, 3, 4, 5 })
        });

        var grid = GridBuilder.Build(reelSet, new[] { 4 }, 3);

        Assert.Equal(new[] { 5, 1, 2 }, grid[0]);
    }
}
