using SlotEngine.Domain.Common;

namespace SlotEngine.Domain.Entities;

public class GameConfigVersion : BaseEntity
{
    public Guid GameId { get; private set; }
    public int Version { get; private set; }
    public string ReelStripsJson { get; private set; } = string.Empty;
    public string PaytableJson { get; private set; } = string.Empty;
    public string PaylinesJson { get; private set; } = string.Empty;
    public string CreatedBy { get; private set; } = string.Empty;

    private GameConfigVersion() { }

    internal GameConfigVersion(Guid gameId, int version, string reelStripsJson, string paytableJson, string paylinesJson, string createdBy)
    {
        if (version <= 0)
            throw new ArgumentOutOfRangeException(nameof(version), "Version must be positive.");
        if (string.IsNullOrWhiteSpace(reelStripsJson))
            throw new ArgumentException("Reel strips JSON is required.", nameof(reelStripsJson));
        if (string.IsNullOrWhiteSpace(paytableJson))
            throw new ArgumentException("Paytable JSON is required.", nameof(paytableJson));
        if (string.IsNullOrWhiteSpace(paylinesJson))
            throw new ArgumentException("Paylines JSON is required.", nameof(paylinesJson));
        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentException("CreatedBy is required.", nameof(createdBy));

        GameId = gameId;
        Version = version;
        ReelStripsJson = reelStripsJson;
        PaytableJson = paytableJson;
        PaylinesJson = paylinesJson;
        CreatedBy = createdBy;
    }
}
