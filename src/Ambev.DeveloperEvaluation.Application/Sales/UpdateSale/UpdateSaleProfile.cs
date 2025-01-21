using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Profile for mapping UpdateSale commands and results.
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes mappings for the UpdateSale feature.
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(dest => dest.Items, opt => opt.Ignore());

        CreateMap<Sale, UpdateSaleResult>();
    }
}
