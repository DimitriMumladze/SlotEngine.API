using SlotEngine.Domain.Common;
using SlotEngine.Domain.Enums;

namespace SlotEngine.Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid PlayerId { get; private set; }
    public TransactionType Type { get; private set; }
    public long Delta { get; private set; }
    public long BalanceAfter { get; private set; }
    public Guid? SpinRecordId { get; private set; }
    public string? Reason { get; private set; }

    private Transaction() { }

    public Transaction(Guid playerId, TransactionType type, long delta, long balanceAfter, Guid? spinRecordId = null, string? reason = null)
    {
        if (balanceAfter < 0)
            throw new ArgumentOutOfRangeException(nameof(balanceAfter), "Resulting balance cannot be negative.");

        PlayerId = playerId;
        Type = type;
        Delta = delta;
        BalanceAfter = balanceAfter;
        SpinRecordId = spinRecordId;
        Reason = reason;
    }
}
