using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Enums;

namespace ShelfTrackPro.Domain.Interfaces;

public interface IStockMovementRepository : IRepository<StockMovement>
{
    Task<IEnumerable<StockMovement>> GetByProductAsync(Guid productId);
    Task<IEnumerable<StockMovement>> GetByTypeAsync(MovementType type);
    Task<IEnumerable<StockMovement>> GetRecentMovementsAsync(int count);
}