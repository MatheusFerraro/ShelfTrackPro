using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfTrackPro.Domain.Entities;

namespace ShelfTrackPro.Infrastructure.Data.Configurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Notes)
            .HasMaxLength(500);

        builder.Property(m => m.Type)
            .HasConversion<string>()
            .HasMaxLength(20);

        // Indexes
        builder.HasIndex(m => m.ProductId);
        builder.HasIndex(m => m.Type);
        builder.HasIndex(m => m.CreatedAt);  // For time-series queries: "show movements from last 7 days"

        // Relationship
        builder.HasOne(m => m.Product)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(m => m.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}