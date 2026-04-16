using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Enums;
using ShelfTrackPro.Infrastructure.Data;

namespace ShelfTrackPro.Infrastructure.Data.Seed;

/// <summary>
/// Seeds the database with demo data for development and Swagger testing.
/// Only inserts data if the tables are empty — safe to call on every startup.
/// </summary>
public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Apply any pending migrations automatically in development
        await context.Database.MigrateAsync();

        // Only seed if database is empty
        if (await context.Categories.AnyAsync())
            return;

        // ─── Categories ───
        var electronics = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electronics",
            Description = "Televisions, computers, phones, and accessories"
        };

        var groceries = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Groceries",
            Description = "Food, beverages, and household essentials"
        };

        var clothing = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Clothing",
            Description = "Apparel, footwear, and accessories"
        };

        await context.Categories.AddRangeAsync(electronics, groceries, clothing);

        // ─── Suppliers ───
        var samsungSupplier = new Supplier
        {
            Id = Guid.NewGuid(),
            Name = "Samsung Distribution",
            Email = "orders@samsung-dist.example.com",
            Phone = "+1-800-555-0101",
            Address = "123 Tech Park, Seoul"
        };

        var localFoods = new Supplier
        {
            Id = Guid.NewGuid(),
            Name = "Atlantic Fresh Foods",
            Email = "supply@atlanticfresh.example.com",
            Phone = "+1-506-555-0202",
            Address = "45 Market St, Moncton, NB"
        };

        await context.Suppliers.AddRangeAsync(samsungSupplier, localFoods);

        // ─── Products ───
        var tv = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Samsung 55\" 4K Smart TV",
            SKU = "ELEC-TV-0001",
            Description = "55-inch Crystal UHD 4K Smart Television",
            Price = 499.99m,
            StockQuantity = 25,
            MinimumStock = 5,
            CategoryId = electronics.Id,
            SupplierId = samsungSupplier.Id
        };

        var headphones = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Wireless Noise-Cancelling Headphones",
            SKU = "ELEC-HP-0002",
            Description = "Over-ear Bluetooth headphones with ANC",
            Price = 149.99m,
            StockQuantity = 3,     // Below minimum — will trigger low-stock alert!
            MinimumStock = 10,
            CategoryId = electronics.Id,
            SupplierId = samsungSupplier.Id
        };

        var coffee = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Premium Ground Coffee 1kg",
            SKU = "GROC-CF-0001",
            Description = "Medium roast Arabica coffee beans, ground",
            Price = 12.99m,
            StockQuantity = 150,
            MinimumStock = 30,
            CategoryId = groceries.Id,
            SupplierId = localFoods.Id
        };

        var jacket = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Winter Parka Jacket",
            SKU = "CLTH-JK-0001",
            Description = "Waterproof insulated winter jacket, -30°C rated",
            Price = 189.99m,
            StockQuantity = 8,
            MinimumStock = 10,     // Also low stock — good for testing alerts
            CategoryId = clothing.Id,
            SupplierId = localFoods.Id
        };

        await context.Products.AddRangeAsync(tv, headphones, coffee, jacket);

        // ─── Admin User (password will be hashed properly in AuthService) ───
        var admin = new User
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            Email = "admin@shelftrackpro.com",
            // This is BCrypt hash of "Admin@123" — in production, NEVER hardcode passwords
            // We'll replace this with proper hashing when we build the AuthService
            PasswordHash = "$2a$11$placeholder_hash_replace_later",
            Role = UserRole.Admin
        };

        await context.Users.AddAsync(admin);

        // ─── Save everything ───
        await context.SaveChangesAsync();
    }
}