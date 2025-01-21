using Ambev.DeveloperEvaluation.Common.Models;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Result model for the ListSales query.
/// </summary>
public class ListSalesResult
{
    /// <summary>
    /// The paginated list of sales.
    /// </summary>
    public PaginatedListModel<SaleResult> Sales { get; set; } = new PaginatedListModel<SaleResult>([], 0, 1, 10);
}

/// <summary>
/// Represents a sale in the ListSalesResult.
/// </summary>
public class SaleResult
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}
