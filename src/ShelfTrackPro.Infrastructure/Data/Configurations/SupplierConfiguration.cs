using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfTrackPro.Domain.Entities;

namespace ShelfTrackPro.Infrastructure.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Email)
            .HasMaxLength(250);

        builder.Property(s => s.Phone)
            .HasMaxLength(30);

        builder.Property(s => s.Address)
            .HasMaxLength(500);

        builder.HasIndex(s => s.Name)
            .IsUnique();
    }
}