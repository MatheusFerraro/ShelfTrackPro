using Microsoft.EntityFrameworkCore;
using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Interfaces;
using ShelfTrackPro.Infrastructure.Data;

namespace ShelfTrackPro.Infrastructure.Repositories;
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
       return await _dbSet
           .Include(p => p.Category) // Eager load — loads Category in the same SQL query (JOIN)
           .Include(p => p.Supplier)
           .FirstOrDefaultAsync(p => p.SKU == sku);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId && p.IsActive)
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
    {
       // Can't use IsLowStock here because it's a C# property, not a DB column.
        // We replicate the logic: StockQuantity <= MinimumStock
        return await _dbSet
            .Where(p => p.IsActive && p.StockQuantity <= p.MinimumStock)
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync(int page, int pageSize)
    {
        return await _dbSet
            .Where(p => p.IsActive)
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)   // Pagination: page 1 skips 0, page 2 skips pageSize, etc.
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> SkuExistsAsync(string sku)
    {
        return await _dbSet.AnyAsync(p => p.SKU == sku);
    }

    public async Task<int> CountActiveAsync()
    {
        return await _dbSet.CountAsync(p => p.IsActive);
    }
}