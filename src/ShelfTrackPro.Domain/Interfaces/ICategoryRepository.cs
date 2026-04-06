using ShelfTrackPro.Domain.Entities;
namespace ShelfTrackPro.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
    Task<bool> NameExistsAsync(string name);
}