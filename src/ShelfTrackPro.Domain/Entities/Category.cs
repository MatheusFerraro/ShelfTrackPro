using ShelfTrackPro.Domain.Common;

namespace ShelfTrackPro.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation property - one category has many products
    public ICollection<Product> Products { get; set; } = new List<Product>();
}