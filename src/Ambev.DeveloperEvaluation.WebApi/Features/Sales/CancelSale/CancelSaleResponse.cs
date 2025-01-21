namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// API response model for CancelSale operation.
/// </summary>
public class CancelSaleResponse
{
    /// <summary>
    /// The unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A message confirming the cancellation of the sale.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
