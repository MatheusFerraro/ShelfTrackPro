using System.Linq.Expressions;
using ShelfTrackPro.Domain.Common;

namespace ShelfTrackPro.Domain.Interfaces;

/// <summary>
/// Generic repository interface with common CRUD operations.
/// Every specific repository (IProductRepository, etc.) extends this.
///
/// Why generic? Avoids repeating GetByIdAsync, AddAsync, etc. in every repository.
/// The "where T : BaseEntity" constraint ensures only domain entities can be used.
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> ExistsAsync(Guid id);
    Task<int> CountAsync();
}