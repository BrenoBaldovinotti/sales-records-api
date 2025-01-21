namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event triggered when a sale is cancelled.
/// </summary>
public class SaleCancelledEvent : BaseEvent
{
    public Guid SaleId { get; }
    public DateTime CancelledAt { get; }

    public SaleCancelledEvent(Guid saleId)
    {
        SaleId = saleId;
        CancelledAt = DateTime.UtcNow;
    }
}
