namespace ShelfTrackPro.Domain.Exceptions;

/// <summary>
/// Thrown when an entity is not found by its ID.
/// The API layer's GlobalExceptionHandler catches this and returns 404 Not Found.
/// </summary>
public class NotFoundException : DomainException
{
    public NotFoundException(string entityName, Guid id)
        : base($"{entityName} with ID '{id}' was not found.") { }
}