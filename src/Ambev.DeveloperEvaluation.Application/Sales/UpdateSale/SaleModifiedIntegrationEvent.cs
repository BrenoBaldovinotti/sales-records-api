using Ambev.DeveloperEvaluation.Application.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{

    /// <summary>
    /// Integration event triggered when a sale is modified.
    /// </summary>
    public class SaleModifiedIntegrationEvent : BaseIntegrationEvent
    {
        public Guid SaleId { get; }

        public SaleModifiedIntegrationEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}