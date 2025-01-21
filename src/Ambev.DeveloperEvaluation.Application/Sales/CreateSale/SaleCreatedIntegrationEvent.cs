using Ambev.DeveloperEvaluation.Application.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{

    /// <summary>
    /// Integration event triggered when a sale is created.
    /// </summary>
    public class SaleCreatedIntegrationEvent : BaseIntegrationEvent
    {
        public Guid SaleId { get; }
        public string SaleNumber { get; }

        public SaleCreatedIntegrationEvent(Guid saleId, string saleNumber)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
        }
    }
}
