namespace ShelfTrackPro.Domain.Interfaces;

/// <summary>
/// Unit of Work pattern — ensures multiple repository operations
/// are saved as a single database transaction (transação única).
///
/// Without this: each repository calls SaveChanges independently.
///   → If AddProduct succeeds but AddStockMovement fails, DB is inconsistent.
///
/// With this: the service calls _unitOfWork.SaveChangesAsync() once at the end.
///   → Either ALL changes are saved, or NONE are. This is atomicity (atomicidade).
///
/// Example usage in a service:
///   await _productRepository.AddAsync(product);
///   await _stockMovementRepository.AddAsync(movement);
///   await _unitOfWork.SaveChangesAsync();  // One transaction for both
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}