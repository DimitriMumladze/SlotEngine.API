using SlotEngine.Domain.Exceptions;

namespace SlotEngine.Domain.ValueObjects;

public readonly record struct Bet
{
    public Coins Amount { get; }
    public int LineCount { get; }

    public Bet(Coins amount, int lineCount)
    {
        if (lineCount <= 0)
            throw new InvalidBetException("Bet must cover at least one payline.");
        if (amount.Amount <= 0)
            throw new InvalidBetException("Bet amount must be positive.");
        if (amount.Amount % lineCount != 0)
            throw new InvalidBetException($"Bet amount {amount.Amount} must be a multiple of line count {lineCount}.");

        Amount = amount;
        LineCount = lineCount;
    }

    public Coins PerLine => new(Amount.Amount / LineCount);
}
