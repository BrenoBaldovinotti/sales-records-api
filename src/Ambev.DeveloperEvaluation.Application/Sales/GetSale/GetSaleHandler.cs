using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler for processing GetSaleByIdCommand requests.
/// </summary>
public class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetSaleByIdHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null) throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");

        return _mapper.Map<GetSaleResult>(sale);
    }
}
