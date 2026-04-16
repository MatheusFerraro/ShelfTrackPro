using Microsoft.EntityFrameworkCore;
using ShelfTrackPro.Domain.Entities;

namespace ShelfTrackPro.Infrastructure.Data;

/// <summary>
/// EF Core DbContext — the central class that manages database connections,
/// entity tracking, migrations, and query translation.
///
/// Think of it as the equivalent of Spring Boot's EntityManager + all your @Entity classes combined.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Each DbSet = a table in SQL Server
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<User> Users => Set<User>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Applies ALL IEntityTypeConfiguration classes from this assembly automatically
    // Instead of calling modelBuilder.Entity<Product>().HasKey(...) here for every entity,
    // we put each entity's config in its own file (separation of concerns / separação de responsabilidades)
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }

  /// <summary>
  /// Override SaveChangesAsync to automatically set UpdatedAt timestamps.
  /// Every time an entity is modified, UpdatedAt gets the current UTC time.
  /// </summary>
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    foreach (var entry in ChangeTracker.Entries<Domain.Common.BaseEntity>())
    {
      if (entry.State == EntityState.Modified)
      {
        entry.Entity.UpdatedAt = DateTime.UtcNow;
      }
    }

    return await base.SaveChangesAsync(cancellationToken);
  }


}