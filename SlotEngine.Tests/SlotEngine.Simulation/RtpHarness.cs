using System.Diagnostics;
using SlotEngine.Application.Abstractions;
using SlotEngine.Application.Slots;
using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Simulation;

public static class RtpHarness
{
    public static RtpResult Run(
        long spins,
        ReelSet reelSet,
        Paytable paytable,
        IReadOnlyList<Payline> paylines,
        int wildSymbolId,
        int rowCount,
        long perLineBet,
        IRng rng)
    {
        var bet = new Bet(new Coins(perLineBet * paylines.Count), paylines.Count);
        long totalWagered = 0;
        long totalPaid = 0;
        long hitCount = 0;
        long maxWin = 0;

        var sw = Stopwatch.StartNew();
        for (long i = 0; i < spins; i++)
        {
            var stops = ReelSpinner.Spin(reelSet, rng);
            var grid = GridBuilder.Build(reelSet, stops, rowCount);
            var result = SpinEvaluator.Evaluate(grid, paytable, paylines, bet, wildSymbolId);

            totalWagered += bet.Amount.Amount;
            totalPaid += result.TotalPayout;
            if (result.TotalPayout > 0) hitCount++;
            if (result.TotalPayout > maxWin) maxWin = result.TotalPayout;
        }
        sw.Stop();

        return new RtpResult(spins, totalWagered, totalPaid, hitCount, maxWin, perLineBet, sw.ElapsedMilliseconds);
    }
}
