using SlotEngine.Application.Slots;
using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Application.Tests.Slots;

public class SpinEvaluatorTests
{
    private const int Wild = 0;

    private static Bet BetOf(long perLine, int lines) => new(new Coins(perLine * lines), lines);

    private static Paytable BuildPaytable() => new(new[]
    {
        new PaytableEntry(Wild, 3, 500),
        new PaytableEntry(1, 3, 5),
        new PaytableEntry(2, 3, 10),
        new PaytableEntry(7, 3, 50)
    });

    private static IReadOnlyList<Payline> Lines() => new[]
    {
        new Payline(new[] { 1, 1, 1 }),
        new Payline(new[] { 0, 0, 0 }),
        new Payline(new[] { 2, 2, 2 })
    };

    [Fact]
    public void NoMatches_ReturnsZeroPayout()
    {
        var grid = new[]
        {
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new[] { 7, 8, 9 }
        };

        var result = SpinEvaluator.Evaluate(grid, BuildPaytable(), Lines(), BetOf(10, 3), Wild);

        Assert.Empty(result.Wins);
        Assert.Equal(0, result.TotalPayout);
    }

    [Fact]
    public void ThreeOfAKindOnMiddleRow_PaysPerLineBetTimesMultiplier()
    {
        var grid = new[]
        {
            new[] { 0, 7, 0 },
            new[] { 0, 7, 0 },
            new[] { 0, 7, 0 }
        };

        var result = SpinEvaluator.Evaluate(grid, BuildPaytable(), Lines(), BetOf(10, 3), Wild);

        var middleWin = Assert.Single(result.Wins, w => w.LineIndex == 0);
        Assert.Equal(7, middleWin.SymbolId);
        Assert.Equal(3, middleWin.MatchCount);
        Assert.Equal(50 * 10, middleWin.Payout);
    }

    [Fact]
    public void WildSubstitutesForLeadSymbol()
    {
        var grid = new[]
        {
            new[] { 9, 0, 9 },
            new[] { 9, 2, 9 },
            new[] { 9, 2, 9 }
        };

        var result = SpinEvaluator.Evaluate(grid, BuildPaytable(), Lines(), BetOf(10, 3), Wild);

        var win = Assert.Single(result.Wins, w => w.LineIndex == 0);
        Assert.Equal(2, win.SymbolId);
        Assert.Equal(3, win.MatchCount);
        Assert.Equal(10 * 10, win.Payout);
    }

    [Fact]
    public void AllWildsLine_PaysWildMultiplier()
    {
        var grid = new[]
        {
            new[] { 9, 0, 9 },
            new[] { 9, 0, 9 },
            new[] { 9, 0, 9 }
        };

        var result = SpinEvaluator.Evaluate(grid, BuildPaytable(), Lines(), BetOf(10, 3), Wild);

        var win = Assert.Single(result.Wins, w => w.LineIndex == 0);
        Assert.Equal(Wild, win.SymbolId);
        Assert.Equal(3, win.MatchCount);
        Assert.Equal(500 * 10, win.Payout);
    }

    [Fact]
    public void BreakInLine_StopsCount()
    {
        var grid = new[]
        {
            new[] { 9, 1, 9 },
            new[] { 9, 1, 9 },
            new[] { 9, 9, 9 }
        };

        var result = SpinEvaluator.Evaluate(grid, BuildPaytable(), Lines(), BetOf(10, 3), Wild);

        Assert.DoesNotContain(result.Wins, w => w.LineIndex == 0);
    }

    [Fact]
    public void MultipleLinesWin_TotalIsSum()
    {
        var grid = new[]
        {
            new[] { 7, 1, 2 },
            new[] { 7, 1, 2 },
            new[] { 7, 1, 2 }
        };

        var result = SpinEvaluator.Evaluate(grid, BuildPaytable(), Lines(), BetOf(10, 3), Wild);

        Assert.Equal(3, result.Wins.Count);
        Assert.Equal((50 + 5 + 10) * 10, result.TotalPayout);
    }

    [Fact]
    public void Positions_TrackLeftToRightWinningCells()
    {
        var grid = new[]
        {
            new[] { 9, 7, 9 },
            new[] { 9, 7, 9 },
            new[] { 9, 7, 9 }
        };

        var result = SpinEvaluator.Evaluate(grid, BuildPaytable(), Lines(), BetOf(10, 3), Wild);

        var win = Assert.Single(result.Wins, w => w.LineIndex == 0);
        Assert.Equal(new[]
        {
            new WinPosition(0, 1),
            new WinPosition(1, 1),
            new WinPosition(2, 1)
        }, win.Positions);
    }
}
