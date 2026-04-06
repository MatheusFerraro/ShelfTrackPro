namespace ShelfTrackPro.Domain.Exceptions;

/// <summary>
/// Base exception for all domain rule violations.
/// The API layer's GlobalExceptionHandler catches this and returns 400 Bad Request.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}