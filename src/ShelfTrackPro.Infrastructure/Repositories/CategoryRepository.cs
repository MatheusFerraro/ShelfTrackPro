using Microsoft.EntityFrameworkCore;
using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Interfaces;
using ShelfTrackPro.Infrastructure.Data;

namespace ShelfTrackPro.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await  _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        return await _dbSet.AnyAsync(c => c.Name == name);
    }
}

