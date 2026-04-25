namespace SlotEngine.Domain.ValueObjects;

public readonly record struct Coins
{
    public long Amount { get; }

    public Coins(long amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Coins cannot be negative.");
        Amount = amount;
    }

    public static Coins Zero => new(0);

    public static Coins operator +(Coins left, Coins right) => new(left.Amount + right.Amount);

    public static Coins operator -(Coins left, Coins right)
    {
        if (left.Amount < right.Amount)
            throw new InvalidOperationException("Coin subtraction would produce a negative balance.");
        return new Coins(left.Amount - right.Amount);
    }

    public static bool operator >(Coins left, Coins right) => left.Amount > right.Amount;
    public static bool operator <(Coins left, Coins right) => left.Amount < right.Amount;
    public static bool operator >=(Coins left, Coins right) => left.Amount >= right.Amount;
    public static bool operator <=(Coins left, Coins right) => left.Amount <= right.Amount;

    public override string ToString() => Amount.ToString();
}
