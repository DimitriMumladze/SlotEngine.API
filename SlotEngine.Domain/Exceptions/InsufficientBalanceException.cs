namespace SlotEngine.Domain.Exceptions;

public sealed class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException()
        : base("Player balance is insufficient for the requested operation.") { }

    public InsufficientBalanceException(string message) : base(message) { }
}
