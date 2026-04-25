using SlotEngine.Domain.Common;
using SlotEngine.Domain.Exceptions;
using SlotEngine.Domain.ValueObjects;

namespace SlotEngine.Domain.Entities;

public class Player : BaseEntity
{
    public string Username { get; private set; } = string.Empty;
    public Coins Balance { get; private set; } = Coins.Zero;

    private Player() { }

    public Player(string username, Coins initialBalance)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required.", nameof(username));

        Username = username;
        Balance = initialBalance;
    }

    public void DebitBet(Bet bet)
    {
        if (Balance < bet.Amount)
            throw new InsufficientBalanceException();

        Balance -= bet.Amount;
        SetUpdated();
    }

    public void CreditWin(Coins winnings)
    {
        Balance += winnings;
        SetUpdated();
    }
}
