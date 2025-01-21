namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a sale.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// The unique identifier of the sale to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the customer associated with the sale.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// The date of the sale.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// A collection of items included in the sale.
    /// </summary>
    public List<UpdateSaleItemRequest> Items { get; set; } = [];
}

/// <summary>
/// Represents a request to update an item within a sale.
/// </summary>
public class UpdateSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the sale item to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product sold.
    /// </summary>
    public int Quantity { get; set; }
}
