using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Common.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Profile for mapping ListSales query results to responses.
/// </summary>
public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        // Map application result to API response
        CreateMap<ListSalesResult, ListSalesResponse>()
            .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src.Sales)); // Ensure sales list is mapped

        // Map from API request to application query
        CreateMap<ListSalesRequest, ListSalesQuery>();

        // Map SaleResult to SaleResponse
        CreateMap<SaleResult, SaleResponse>();

        // Map PaginatedListModel<SaleResult> to PaginatedListModel<SaleResponse>
        CreateMap(typeof(PaginatedListModel<>), typeof(PaginatedListModel<>))
            .ConvertUsing(typeof(PaginatedListModelConverter<,>));
    }
}

/// <summary>
/// Converter for PaginatedListModel in API.
/// </summary>
public class PaginatedListModelConverter<TSource, TDestination> : ITypeConverter<PaginatedListModel<TSource>, PaginatedListModel<TDestination>>
{
    private readonly IMapper _mapper;

    public PaginatedListModelConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public PaginatedListModel<TDestination> Convert(
        PaginatedListModel<TSource> source,
        PaginatedListModel<TDestination> destination,
        ResolutionContext context)
    {
        var items = _mapper.Map<List<TDestination>>(source);
        return new PaginatedListModel<TDestination>(items, source.TotalCount, source.CurrentPage, source.PageSize);
    }
}
