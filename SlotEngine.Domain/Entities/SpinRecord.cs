using SlotEngine.Domain.Common;

namespace SlotEngine.Domain.Entities;

public class SpinRecord : BaseEntity
{
    public Guid PlayerId { get; private set; }
    public Guid GameId { get; private set; }
    public Guid GameConfigVersionId { get; private set; }
    public Guid ClientSpinId { get; private set; }
    public long BetAmount { get; private set; }
    public int LineCount { get; private set; }
    public string StopsJson { get; private set; } = string.Empty;
    public string GridJson { get; private set; } = string.Empty;
    public string WinsJson { get; private set; } = string.Empty;
    public long TotalPayout { get; private set; }
    public long BalanceBefore { get; private set; }
    public long BalanceAfter { get; private set; }

    private SpinRecord() { }

    public SpinRecord(
        Guid playerId,
        Guid gameId,
        Guid gameConfigVersionId,
        Guid clientSpinId,
        long betAmount,
        int lineCount,
        string stopsJson,
        string gridJson,
        string winsJson,
        long totalPayout,
        long balanceBefore,
        long balanceAfter)
    {
        if (betAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(betAmount), "Bet amount must be positive.");
        if (lineCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(lineCount), "Line count must be positive.");
        if (totalPayout < 0)
            throw new ArgumentOutOfRangeException(nameof(totalPayout), "Total payout cannot be negative.");

        PlayerId = playerId;
        GameId = gameId;
        GameConfigVersionId = gameConfigVersionId;
        ClientSpinId = clientSpinId;
        BetAmount = betAmount;
        LineCount = lineCount;
        StopsJson = stopsJson;
        GridJson = gridJson;
        WinsJson = winsJson;
        TotalPayout = totalPayout;
        BalanceBefore = balanceBefore;
        BalanceAfter = balanceAfter;
    }
}
