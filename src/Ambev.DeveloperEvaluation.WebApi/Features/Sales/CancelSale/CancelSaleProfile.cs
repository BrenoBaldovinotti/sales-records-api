using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Profile for mapping CancelSale operations.
/// </summary>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Initializes mappings for CancelSale operations.
    /// </summary>
    public CancelSaleProfile()
    {
        CreateMap<Guid, CancelSaleCommand>()
            .ConstructUsing(id => new CancelSaleCommand(id));

        CreateMap<CancelSaleResult, CancelSaleResponse>();
    }
}
