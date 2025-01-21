using Ambev.DeveloperEvaluation.Application.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{

    /// <summary>
    /// Integration event triggered when a sale is cancelled.
    /// </summary>
    public class SaleCancelledIntegrationEvent : BaseIntegrationEvent
    {
        public Guid SaleId { get; }

        public SaleCancelledIntegrationEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}