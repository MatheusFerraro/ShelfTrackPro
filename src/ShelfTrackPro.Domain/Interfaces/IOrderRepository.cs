using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Enums;

namespace ShelfTrackPro.Domain.Interfaces;

public interface IOrderRepository : IRepository<PurchaseOrder>
{
    Task<PurchaseOrder?> GetByIdWithItemsAsync(Guid id);
    Task<IEnumerable<PurchaseOrder>> GetByStatusAsync(OrderStatus status);
    Task<IEnumerable<PurchaseOrder>> GetBySupplierAsync(Guid supplierId);
    Task<IEnumerable<PurchaseOrder>> GetRecentOrdersAsync(int count);
}