using ShelfTrackPro.Domain.Common;
using ShelfTrackPro.Domain.Enums;

namespace ShelfTrackPro.Domain.Entities;

/// <summary>
/// Records every stock change for audit purposes.
/// Every time stock goes up, down, or gets adjusted, a StockMovement is created.
/// This is an append-only table (somente inserção) — movements are never edited or deleted.
/// </summary>
public class StockMovement : BaseEntity
{
    public int Quantity { get; set; }           // Always positive — Type tells direction
    public MovementType Type { get; set; }
    public string? Notes { get; set; }          // Reason for the movement, e.g. "Inventory count correction"

    // Foreign Key
    public Guid ProductId { get; set; }

    // Navigation property
    public Product Product { get; set; } = null!;
}