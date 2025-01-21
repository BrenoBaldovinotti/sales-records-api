using Ambev.DeveloperEvaluation.Common.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// API response model for listing sales.
/// </summary>
public class ListSalesResponse
{
    /// <summary>
    /// Paginated sales data.
    /// </summary>
    public PaginatedListModel<SaleResponse> Sales { get; set; } = new PaginatedListModel<SaleResponse>([], 0, 1, 10);
}

/// <summary>
/// API response model for an individual sale.
/// </summary>
public class SaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}
