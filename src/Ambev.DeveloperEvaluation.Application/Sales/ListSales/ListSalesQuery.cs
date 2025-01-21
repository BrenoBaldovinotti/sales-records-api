using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Command for listing sales.
/// </summary>
public record ListSalesQuery(int PageNumber, int PageSize) : IRequest<ListSalesResult>;
