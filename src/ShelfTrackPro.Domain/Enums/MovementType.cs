namespace ShelfTrackPro.Domain.Enums;

/// <summary>
/// Represents the type of stock movement for audit trail purposes.
/// </summary>

public enum MovementType
{
   In = 0,             // Stock received (purchase orders, return)
   Out = 1,           // Stock removed (sales, damage, expiry)
   Adjustment = 2     // Manual correction (inventory count fix)
}