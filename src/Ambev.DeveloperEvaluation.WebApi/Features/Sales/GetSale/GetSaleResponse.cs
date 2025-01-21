namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSaleById operation.
/// </summary>
public class GetSaleResponse
{
    /// <summary>
    /// The unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the sale occurred.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The name of the customer associated with the sale.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// The identifier of the branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The total amount for the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Indicates whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// A collection of items included in the sale.
    /// </summary>
    public List<GetSaleItemResponse> Items { get; set; } = [];
}

/// <summary>
/// API response model for a sale item in GetSaleById operation.
/// </summary>
public class GetSaleItemResponse
{
    /// <summary>
    /// The unique identifier of the sale item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product ID associated with the sale item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of the product during the sale.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to this item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total price for this item after applying the discount.
    /// </summary>
    public decimal Total { get; set; }
}
