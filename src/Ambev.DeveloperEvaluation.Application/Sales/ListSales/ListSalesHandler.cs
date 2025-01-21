using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Common.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Handles the ListSalesCommand request.
/// </summary>
public class ListSalesHandler : IRequestHandler<ListSalesQuery, ListSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the ListSalesHandler class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public ListSalesHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListSalesCommand request.
    /// </summary>
    /// <param name="request">The ListSalesCommand.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of sales.</returns>
    public async Task<ListSalesResult> Handle(ListSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetPaginatedAsync(request.PageNumber, request.PageSize, cancellationToken);

        var paginatedSales = _mapper.Map<PaginatedListModel<SaleResult>>(sales);

        return new ListSalesResult
        {
            Sales = paginatedSales
        };
    }
}
