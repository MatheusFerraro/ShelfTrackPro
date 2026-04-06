namespace ShelfTrackPro.Domain.Exceptions;

/// <summary>
/// Thrown when attempting to remove more stock than is available.
/// Example: Product has 5 units, user tries to remove 10.
/// </summary>
public class InsufficientStockException : DomainException
{
    public string ProductName { get; }
    public int AvailableStock { get; }
    public int RequestedQuantity { get; }

     public InsufficientStockException(string productName, int available, int requested)
        : base($"Insufficient stock for '{productName}'. Available: {available}, Requested: {requested}.")
    {
        ProductName = productName;
        AvailableStock = available;
        RequestedQuantity = requested;
    }
}