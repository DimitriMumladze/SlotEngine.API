namespace SlotEngine.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public RecordStatus Status { get; private set; } = RecordStatus.Active;

    public void SetUpdated() => UpdatedAt = DateTime.UtcNow;

    public void Deactivate()
    {
        Status = RecordStatus.Inactive;
        SetUpdated();
    }

    public void Activate()
    {
        Status = RecordStatus.Active;
        SetUpdated();
    }

    public void MarkDeleted()
    {
        Status = RecordStatus.Deleted;
        SetUpdated();
    }
}
