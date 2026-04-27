using SlotEngine.Domain.Configuration;
using SlotEngine.Infrastructure.Random;
using Xunit.Abstractions;

namespace SlotEngine.Simulation;

public class RtpSimulationTests
{
    private const long SpinCount = 1_000_000;
    private const long PerLineBet = 1;

    private readonly ITestOutputHelper _output;

    public RtpSimulationTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void StarterConfig_RtpAndHitFrequency_LandInTargetBands()
    {
        var result = RtpHarness.Run(
            spins: SpinCount,
            reelSet: StarterConfig.BuildReelSet(),
            paytable: StarterConfig.BuildPaytable(),
            paylines: StarterConfig.BuildPaylines(),
            wildSymbolId: StarterConfig.WildSymbolId,
            rowCount: StarterConfig.RowCount,
            perLineBet: PerLineBet,
            rng: new CryptoRng());

        _output.WriteLine($"Spins:           {result.Spins:N0}");
        _output.WriteLine($"Total wagered:   {result.TotalWagered:N0}");
        _output.WriteLine($"Total paid:      {result.TotalPaid:N0}");
        _output.WriteLine($"RTP:             {result.Rtp:P3}");
        _output.WriteLine($"Hit frequency:   {result.HitFrequency:P3}");
        _output.WriteLine($"Hit count:       {result.HitCount:N0}");
        _output.WriteLine($"Max win:         {result.MaxWin:N0} ({result.MaxWinMultiplier:F1}x per-line bet)");
        _output.WriteLine($"Duration:        {result.DurationMs:N0} ms");

        Assert.InRange(result.Rtp, 0.93, 0.97);
        Assert.InRange(result.HitFrequency, 0.17, 0.38);
    }
}
