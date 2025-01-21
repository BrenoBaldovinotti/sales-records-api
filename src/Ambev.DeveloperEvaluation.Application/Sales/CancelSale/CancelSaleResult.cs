namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Result of canceling a sale.
/// </summary>
public class CancelSaleResult
{
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
}
