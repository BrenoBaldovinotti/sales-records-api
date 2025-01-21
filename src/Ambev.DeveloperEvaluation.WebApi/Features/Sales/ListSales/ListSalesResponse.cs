namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

using Ambev.DeveloperEvaluation.Common.Models;

/// <summary>
/// Response model for the ListSales operation.
/// </summary>
public class ListSalesResponse
{
    /// <summary>
    /// The paginated list of sales.
    /// </summary>
    public PaginatedListModel<ListSalesResponse> Sales { get; set; } = new([], 0, 1, 10);
}
