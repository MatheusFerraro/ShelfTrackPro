using Microsoft.EntityFrameworkCore;
using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Enums;
using ShelfTrackPro.Domain.Interfaces;
using ShelfTrackPro.Infrastructure.Data;

namespace ShelfTrackPro.Infrastructure.Repositories;

public class StockMovementRepository : BaseRepository<StockMovement>, IStockMovementRepository
{
    public StockMovementRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<StockMovement>> GetByProductAsync(Guid productId)
    {
        return await _dbSet
            .Where(m => m.ProductId == productId)
            .Include(m => m.Product)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetByTypeAsync(MovementType type)
    {
        return await _dbSet
            .Where(m => m.Type == type)
            .Include(m => m.Product)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetRecentMovementsAsync(int count)
    {
        return await _dbSet
            .Include(m => m.Product)
            .OrderByDescending(m => m.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}