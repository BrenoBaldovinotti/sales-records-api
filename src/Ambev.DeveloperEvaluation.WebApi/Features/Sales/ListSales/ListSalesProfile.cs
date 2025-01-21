using AutoMapper;
using Ambev.DeveloperEvaluation.Common.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

/// <summary>
/// Profile for mapping ListSales results to API responses.
/// </summary>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSales feature.
    /// </summary>
    public ListSalesProfile()
    {
        // Map ListSalesRequest to ListSalesQuery
        CreateMap<ListSalesRequest, Application.Sales.ListSales.ListSalesQuery>();

        // Map ListSaleResult to ListSaleResponse
        CreateMap<Application.Sales.ListSales.ListSalesResult, ListSalesResponse>();

        // Map PaginatedListModel<ListSaleResult> to PaginatedListModel<ListSaleResponse>
        CreateMap(typeof(PaginatedListModel<>), typeof(PaginatedListModel<>))
            .ConvertUsing(typeof(PaginatedListModelConverter<,>));
    }
}

/// <summary>
/// Converts PaginatedListModel<TSource> to PaginatedListModel<TDestination>.
/// </summary>
public class PaginatedListModelConverter<TSource, TDestination>
    : ITypeConverter<PaginatedListModel<TSource>, PaginatedListModel<TDestination>>
{
    public PaginatedListModel<TDestination> Convert(
        PaginatedListModel<TSource> source,
        PaginatedListModel<TDestination> destination,
        ResolutionContext context)
    {
        var mappedItems = context.Mapper.Map<List<TDestination>>(source);

        return new PaginatedListModel<TDestination>(
            mappedItems,
            source.TotalCount,
            source.CurrentPage,
            source.PageSize
        );
    }
}
