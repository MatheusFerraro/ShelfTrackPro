using ShelfTrackPro.Domain.Entities;

namespace ShelfTrackPro.Domain.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<Supplier?> GetByNameAsync(string name);
    Task<bool> NameExistsAsync(string name);
}