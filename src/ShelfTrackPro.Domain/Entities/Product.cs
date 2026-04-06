using ShelfTrackPro.Domain.Common;

namespace ShelfTrackPro.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty; // Stock Keeping Unit — unique identifier like "ELEC-TV-0042"
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int MinimumStock { get; set; } // Triggers low-stock alert when StockQuantity drops below this
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; } = true; // Soft delete — never physically remove products

    // Optimistic Concurrency 
    // EF Core checks this value on UPDATE - if another request changed it first, throws DbUpdateConcurrencyException
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    // Foreign keys
    public Guid CategoryId { get; set; }
    public Guid SupplierId { get; set; }

    // Navigation properties
    public Category Category { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

    // ─── Domain Logic (business rules live HERE, not in services) ───

    /// <summary>
    /// Returns true when stock is at or below the minimum threshold.
    /// </summary>
    public bool IsLowStock => StockQuantity <= MinimumStock;

    /// <summary>
    /// Adds stock and validates the quantity is positive.
    /// </summary>
    public void AddStock(int quantity)
    {
        if (quantity <= 0) 
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));
        
        StockQuantity += quantity;
    }

     /// <summary>
    /// Removes stock. Throws if there's not enough available.
    /// </summary>
    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));

        if (quantity > StockQuantity)
            throw new Exceptions.InsufficientStockException(Name, StockQuantity, quantity);

        StockQuantity -= quantity;
    }
     
     

   
}
