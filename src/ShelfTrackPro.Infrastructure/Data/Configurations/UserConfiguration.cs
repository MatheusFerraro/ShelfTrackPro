using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfTrackPro.Domain.Entities;

namespace ShelfTrackPro.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);  // BCrypt hashes are ~60 chars, but extra space for future algorithms

        // Store enum as string in the database for readability
        // Without this, EF Core stores 0, 1, 2 — which is harder to debug in SQL queries
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        // Unique email — no two users with the same email
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}