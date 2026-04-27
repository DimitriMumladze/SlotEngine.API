using SlotEngine.Domain.Common;

namespace SlotEngine.Domain.Entities;

public class Game : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public int ReelCount { get; private set; }
    public int RowCount { get; private set; }
    public Guid? CurrentConfigVersionId { get; private set; }

    private readonly List<GameConfigVersion> _configVersions = new();
    public IReadOnlyList<GameConfigVersion> ConfigVersions => _configVersions.AsReadOnly();

    private Game() { }

    public Game(string name, int reelCount, int rowCount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Game name is required.", nameof(name));
        if (reelCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(reelCount), "Reel count must be positive.");
        if (rowCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(rowCount), "Row count must be positive.");

        Name = name;
        ReelCount = reelCount;
        RowCount = rowCount;
    }

    public GameConfigVersion PublishConfigVersion(string reelStripsJson, string paytableJson, string paylinesJson, string createdBy)
    {
        var nextVersion = _configVersions.Count == 0 ? 1 : _configVersions.Max(v => v.Version) + 1;
        var configVersion = new GameConfigVersion(Id, nextVersion, reelStripsJson, paytableJson, paylinesJson, createdBy);
        _configVersions.Add(configVersion);
        CurrentConfigVersionId = configVersion.Id;
        SetUpdated();
        return configVersion;
    }
}
