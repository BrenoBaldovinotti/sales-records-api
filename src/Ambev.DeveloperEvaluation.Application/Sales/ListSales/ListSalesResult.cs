using Ambev.DeveloperEvaluation.Common.Models;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Result model for the ListSales operation.
/// </summary>
public class ListSalesResult
{
    /// <summary>
    /// A paginated list of sales.
    /// </summary>
    public PaginatedListModel<ListSalesResult> Sales { get; set; } = new([], 0, 1, 10);
}
