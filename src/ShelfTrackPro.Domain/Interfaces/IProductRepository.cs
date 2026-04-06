using ShelfTrackPro.Domain.Entities;

namespace ShelfTrackPro.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySkuAsync(string sku);
    Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Product>> GetLowStockProductsAsync();
    Task<IEnumerable<Product>> GetActiveProductsAsync(int page, int pageSize);
    Task<bool> SkuExistsAsync(string sku);
    Task<int> CountActiveAsync();
}