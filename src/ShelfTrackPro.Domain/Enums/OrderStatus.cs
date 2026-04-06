namespace ShelfTrackPro.Domain.Enums;

/// <summary>
/// Represents the lifecycle stages of a purchase order.
/// Flow: Pending → Approved → Received (or Cancelled at any point)
/// </summary>
public enum OrderStatus
{
    Pending = 0,   
    Approved = 1,  
    Received = 2,  
    Cancelled = 3  
}