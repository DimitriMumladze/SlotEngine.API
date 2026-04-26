using SlotEngine.Application.Slots;
using SlotEngine.Application.Tests.Fakes;
using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Application.Tests.Slots;

public class ReelSpinnerTests
{
    [Fact]
    public void Spin_ReturnsOneStopPerReel_FromScriptedRng()
    {
        var reelSet = new ReelSet(new[]
        {
            new ReelStrip(new[] { 1, 2, 3, 4, 5 }),
            new ReelStrip(new[] { 6, 7, 8, 9, 10 }),
            new ReelStrip(new[] { 11, 12, 13, 14, 15 })
        });
        var rng = new FakeRng(2, 0, 4);

        var stops = ReelSpinner.Spin(reelSet, rng);

        Assert.Equal(new[] { 2, 0, 4 }, stops);
    }
}
