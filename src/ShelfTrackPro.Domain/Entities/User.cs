using ShelfTrackPro.Domain.Common;
using ShelfTrackPro.Domain.Enums;

namespace ShelfTrackPro.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; //  NEVER store plain text — BCrypt hash only
    public UserRole Role { get; set; } = UserRole.Viewer; 

    // Navigation property
    public ICollection<PurchaseOrder> CreatedOrders { get; set; } = new List<PurchaseOrder>();
}