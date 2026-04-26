namespace SlotEngine.Application.Slots;

public readonly record struct WinPosition(int Reel, int Row);

public sealed record WinLine(
    int LineIndex,
    int SymbolId,
    int MatchCount,
    long Payout,
    IReadOnlyList<WinPosition> Positions);

public sealed record WinResult(IReadOnlyList<WinLine> Wins, long TotalPayout);
