using Microsoft.EntityFrameworkCore;
using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Interfaces;
using ShelfTrackPro.Infrastructure.Data;

namespace ShelfTrackPro.Infrastructure.Repositories;

public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(AppDbContext context) : base(context) { }

    public async Task<Supplier?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        return await _dbSet.AnyAsync(s => s.Name == name);
    }
}