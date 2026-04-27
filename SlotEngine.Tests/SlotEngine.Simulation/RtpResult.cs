namespace SlotEngine.Simulation;

public sealed record RtpResult(
    long Spins,
    long TotalWagered,
    long TotalPaid,
    long HitCount,
    long MaxWin,
    long PerLineBet,
    long DurationMs)
{
    public double Rtp => TotalWagered == 0 ? 0 : (double)TotalPaid / TotalWagered;
    public double HitFrequency => Spins == 0 ? 0 : (double)HitCount / Spins;
    public double MaxWinMultiplier => PerLineBet == 0 ? 0 : (double)MaxWin / PerLineBet;
}
