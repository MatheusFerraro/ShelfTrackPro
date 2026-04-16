using Microsoft.EntityFrameworkCore;
using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Enums;
using ShelfTrackPro.Domain.Interfaces;
using ShelfTrackPro.Infrastructure.Data;

namespace ShelfTrackPro.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<PurchaseOrder>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<PurchaseOrder?> GetByIdWithItemsAsync(Guid id)
    {
        // Eager load the full object graph:
        // PurchaseOrder → Items → Product (for each item)
        // PurchaseOrder → Supplier
        // PurchaseOrder → CreatedBy (User)
        return await _dbSet
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)  // ThenInclude = nested eager loading 
            .Include(o => o.Supplier)
            .Include(o => o.CreatedBy)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetByStatusAsync(OrderStatus status)
    {
        return await _dbSet
            .Where(o => o.Status == status)
            .Include(o => o.Supplier)
            .Include(o => o.CreatedBy)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<PurchaseOrder>> GetBySupplierAsync(Guid supplierId)
    {
        return await _dbSet
            .Where(o => o.SupplierId == supplierId)
            .Include(o => o.Supplier)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<PurchaseOrder>> GetRecentOrdersAsync(int count)
    {
        return await _dbSet
            .Include(o => o.Supplier)
            .Include(o => o.CreatedBy)
            .OrderByDescending(o => o.OrderDate)
            .Take(count)
            .ToListAsync();
    }
}