namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event triggered when a sale is modified.
/// </summary>
public class SaleModifiedEvent : BaseEvent
{
    public Guid SaleId { get; }
    public DateTime ModifiedAt { get; }

    public SaleModifiedEvent(Guid saleId)
    {
        SaleId = saleId;
        ModifiedAt = DateTime.UtcNow;
    }
}
