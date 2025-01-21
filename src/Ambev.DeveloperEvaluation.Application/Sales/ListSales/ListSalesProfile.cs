using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Models;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Profile for mapping ListSalesQuery and ListSalesResult.
/// </summary>
public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        // Map Sale entity to SaleResult
        CreateMap<Sale, SaleResult>();

        // Map PaginatedListModel<Sale> to PaginatedListModel<SaleResult>
        CreateMap(typeof(PaginatedListModel<>), typeof(PaginatedListModel<>))
            .ConvertUsing(typeof(PaginatedListModelConverter<,>));
    }
}

/// <summary>
/// Converter for PaginatedListModel.
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
