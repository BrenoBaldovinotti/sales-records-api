namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event triggered when a sale is created.
/// </summary>
public class SaleCreatedEvent : BaseEvent
{
    public Guid SaleId { get; }
    public string SaleNumber { get; }
    public DateTime CreatedAt { get; }

    public SaleCreatedEvent(Guid saleId, string saleNumber)
    {
        SaleId = saleId;
        SaleNumber = saleNumber;
        CreatedAt = DateTime.UtcNow;
    }
}
