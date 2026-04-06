namespace ShelfTrackPro.Domain.Common;

/// <summary>
/// Base class for all domain entities.
/// Contains shared properties like Id and audit timestamps.
/// </summary>

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}