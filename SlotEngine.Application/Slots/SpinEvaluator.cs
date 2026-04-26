using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Application.Slots;

public static class SpinEvaluator
{
    public static WinResult Evaluate(
        int[][] grid,
        Paytable paytable,
        IReadOnlyList<Payline> paylines,
        Bet bet,
        int wildSymbolId)
    {
        ArgumentNullException.ThrowIfNull(grid);
        ArgumentNullException.ThrowIfNull(paytable);
        ArgumentNullException.ThrowIfNull(paylines);

        var perLine = bet.PerLine.Amount;
        var wins = new List<WinLine>();
        long total = 0;

        for (var i = 0; i < paylines.Count; i++)
        {
            var line = paylines[i];
            var win = EvaluateLine(grid, line, paytable, perLine, wildSymbolId, i);
            if (win is null) continue;
            wins.Add(win);
            total += win.Payout;
        }

        return new WinResult(wins, total);
    }

    private static WinLine? EvaluateLine(
        int[][] grid,
        Payline line,
        Paytable paytable,
        long perLineBet,
        int wildSymbolId,
        int lineIndex)
    {
        var reelCount = grid.Length;
        if (line.Length != reelCount)
            throw new ArgumentException("Payline length must equal reel count.", nameof(line));

        int? leadSymbol = null;
        for (var reel = 0; reel < reelCount; reel++)
        {
            var sym = grid[reel][line.RowAt(reel)];
            if (sym != wildSymbolId) { leadSymbol = sym; break; }
        }

        var winSymbol = leadSymbol ?? wildSymbolId;

        var matchCount = 0;
        for (var reel = 0; reel < reelCount; reel++)
        {
            var sym = grid[reel][line.RowAt(reel)];
            if (sym == winSymbol || sym == wildSymbolId) matchCount++;
            else break;
        }

        var multiplier = paytable.Multiplier(winSymbol, matchCount);
        if (multiplier == 0) return null;

        var positions = new WinPosition[matchCount];
        for (var reel = 0; reel < matchCount; reel++)
            positions[reel] = new WinPosition(reel, line.RowAt(reel));

        return new WinLine(lineIndex, winSymbol, matchCount, multiplier * perLineBet, positions);
    }
}
