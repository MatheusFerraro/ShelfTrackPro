using Microsoft.EntityFrameworkCore;
using ShelfTrackPro.Domain.Entities;
using ShelfTrackPro.Domain.Interfaces;
using ShelfTrackPro.Infrastructure.Data;

namespace ShelfTrackPro.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }
}