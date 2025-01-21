using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Profile for mapping between Sale entity and GetSaleByIdResult.
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSaleById operation.
    /// </summary>
    public GetSaleProfile()
    {
        // Map from Sale domain entity to application result.
        CreateMap<Sale, GetSaleResult>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map from SaleItem domain entity to application result.
        CreateMap<SaleItem, GetSaleItemResult>();
    }
}
