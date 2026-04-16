using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfTrackPro.Domain.Entities;

namespace ShelfTrackPro.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the Product table in SQL Server.
/// Equivalent to JPA's @Entity/@Column/@Table annotations, but defined externally
/// so the Domain entity stays framework-free (livre de framework).
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Table name
        builder.ToTable("Products");

        // Primary key
        builder.HasKey(p => p.Id); 

        // Properties
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.SKU)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Price)
            .HasPrecision(18, 2);  // 18 digits total, 2 after decimal → max 9999999999999999.99

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(500);
        
        // Optimistic Concurrency — SQL Server generates and updates this automatically
        builder.Property(p => p.RowVersion)
            .IsRowVersion(); // Maps to SQL Server's ROWVERSION type
        
        // Indexes
        builder.HasIndex(p => p.SKU)
            .IsUnique(); // No two products can have the same SKU
        
        builder.HasIndex(p => p.IsActive);  // Speeds up filtering active products
        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.SupplierId);

        // Relationships
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete — can't delete a category that has products
        
        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);


        // Ignore computed properties — EF Core should NOT create a column for these
        builder.Ignore(p => p.IsLowStock);
    }
}