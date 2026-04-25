namespace SlotEngine.Domain.Exceptions;

public sealed class InvalidBetException : Exception
{
    public InvalidBetException(string message) : base(message) { }
}
