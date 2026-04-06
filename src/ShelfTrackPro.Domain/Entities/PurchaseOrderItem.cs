using ShelfTrackPro.Domain.Common;

namespace ShelfTrackPro.Domain.Entities;

/// <summary>
/// A single line item within a PurchaseOrder.
/// Example: "50 units of Samsung TV at $499.99 each"
/// </summary>
public class PurchaseOrderItem : BaseEntity
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } 

    // Computed property - no need to store in DB, EF core ignores it by default
    public decimal LineTotal => Quantity * UnitPrice;

     // Foreign Keys
    public Guid PurchaseOrderId { get; set; }
    public Guid ProductId { get; set; }

    // Navigation properties
    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public Product Product { get; set; } = null!;
    
}

