using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Profile for mapping GetSale feature requests to commands and results to responses.
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale feature.
    /// </summary>
    public GetSaleProfile()
    {
        // Map from GetSaleResult to GetSaleResponse
        CreateMap<Application.Sales.GetSaleById.GetSaleResult, GetSaleResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map from GetSaleItemResult to GetSaleItemResponse
        CreateMap<Application.Sales.GetSaleById.GetSaleItemResult, GetSaleItemResponse>();
    }
}
