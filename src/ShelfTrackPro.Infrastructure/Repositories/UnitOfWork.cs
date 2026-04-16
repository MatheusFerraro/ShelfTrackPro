using ShelfTrackPro.Domain.Interfaces;
using ShelfTrackPro.Infrastructure.Data;

namespace ShelfTrackPro.Infrastructure.Repositories;

/// <summary>
/// Unit of Work — wraps DbContext.SaveChangesAsync().
///
/// Why not just call _context.SaveChangesAsync() directly in each repository?
/// Because sometimes a service needs to save multiple things atomically (atomicamente):
///
///   await _productRepo.AddAsync(product);
///   await _stockMovementRepo.AddAsync(movement);
///   await _unitOfWork.SaveChangesAsync();  // ONE transaction for both
///
/// If the second operation fails, the first is rolled back (revertida).
///
/// Compare with Spring Boot's @Transactional annotation — same concept,
/// but explicit instead of declarative (explícito em vez de declarativo).
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}