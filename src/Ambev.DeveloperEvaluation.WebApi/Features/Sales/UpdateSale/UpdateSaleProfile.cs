using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Profile for mapping UpdateSale requests and responses.
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes mappings for the UpdateSale feature.
    /// </summary>
    public UpdateSaleProfile()
    {
        // Map from UpdateSaleRequest to UpdateSaleCommand
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        // Map from UpdateSaleItemRequest to UpdateSaleItemCommand
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();

        CreateMap<UpdateSaleResult, UpdateSaleResponse>();
    }
}
