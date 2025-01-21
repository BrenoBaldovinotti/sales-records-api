using AutoMapper;
using Ambev.DeveloperEvaluation.Common.Models;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Profile for mapping ListSales entities to results.
/// </summary>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListSales.
    /// </summary>
    public ListSalesProfile()
    {
        // Map Sale to ListSaleResult
        CreateMap<Domain.Entities.Sale, ListSalesResult>();

        // Map PaginatedListModel<Sale> to PaginatedListModel<ListSaleResult>
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
