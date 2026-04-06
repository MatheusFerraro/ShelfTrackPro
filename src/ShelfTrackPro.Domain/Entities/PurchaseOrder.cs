using ShelfTrackPro.Domain.Common;
using ShelfTrackPro.Domain.Enums;
using ShelfTrackPro.Domain.Exceptions;

namespace ShelfTrackPro.Domain.Entities;

public class PurchaseOrder : BaseEntity
{
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string? Notes { get; set; }
    public decimal TotalAmount { get; set; } 
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovedDate { get; set; }
    public DateTime? ReceivedDate { get; set; }

    // Foreign keys
    public Guid SupplierId { get; set; }
    public Guid CreatedByUserId { get; set; }

    // Navigation properties
    public Supplier Supplier { get; set; } = null!;
    public User CreatedBy { get; set; } = null!;
    public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();

     // ─── Domain Logic (state machine rules) ───

    public void Approve()
    {
        if (Status != OrderStatus.Pending)
            throw new DomainException($"Cannot approve order with status '{Status}'. Only Pending orders can be approved.");

        Status = OrderStatus.Approved;
        ApprovedDate = DateTime.UtcNow;

    }

    public void MarkAsReceived()
    {
        if (Status != OrderStatus.Approved)
            throw new DomainException($"Cannot receive order with status '{Status}'. Only Approved orders can be received.");

        Status = OrderStatus.Received;
        ReceivedDate = DateTime.UtcNow;
        
    }

      public void Cancel()
    {
        if (Status == OrderStatus.Received)
            throw new DomainException("Cannot cancel an order that has already been received.");

        if (Status == OrderStatus.Cancelled)
            throw new DomainException("Order is already cancelled.");

        Status = OrderStatus.Cancelled;
    }

    public void RecalculateTotal()
    {
        TotalAmount = Items.Sum(item => item.Quantity * item.UnitPrice);
    }
}