using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Common.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Unit tests for the <see cref="ListSalesHandler"/> class.
/// </summary>
public class ListSalesHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ListSalesHandler _handler;

    /// <summary>
    /// Initializes a new instance of <see cref="ListSalesHandlerTests"/>.
    /// </summary>
    public ListSalesHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListSalesHandler(_saleRepository, _mapper);
    }

    /// <summary>
    /// Tests that the handler returns a paginated list of sales successfully.
    /// </summary>
    [Fact(DisplayName = "Handle valid query and return paginated sales list")]
    public async Task Handle_ValidQuery_ReturnsPaginatedSalesList()
    {
        // Arrange
        var query = new ListSalesQuery(1, 10);

        var sales = new PaginatedListModel<Sale>(
            [
            new Sale { Id = Guid.NewGuid(), Customer = "Customer A" },
            new Sale { Id = Guid.NewGuid(), Customer = "Customer B" }
            ],
            2,
            1,
            2
        );

        var paginatedMappedSales = new PaginatedListModel<SaleResult>(
            [
            new SaleResult { Id = sales[0].Id, Customer = "Customer A" },
            new SaleResult { Id = sales[1].Id, Customer = "Customer B" }
            ],
            2,
            1,
            2
        );

        var mappedResult = new ListSalesResult
        {
            Sales = paginatedMappedSales
        };

        _saleRepository.GetPaginatedAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(sales);

        // Mocking the mapping of PaginatedListModel<SaleResult>
        _mapper.Map<PaginatedListModel<SaleResult>>(sales).Returns(paginatedMappedSales);

        // Mocking the overall result mapping
        _mapper.Map<ListSalesResult>(sales).Returns(mappedResult);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Sales.Count.Should().Be(2);
        result.Sales[0].Customer.Should().Be("Customer A");
        result.Sales[1].Customer.Should().Be("Customer B");

        await _saleRepository.Received(1)
            .GetPaginatedAsync(query.PageNumber, query.PageSize, Arg.Any<CancellationToken>());
    }


    /// <summary>
    /// Tests that the handler throws an exception when an unexpected error occurs.
    /// </summary>
    [Fact(DisplayName = "Handle unexpected repository exception")]
    public async Task Handle_RepositoryError_ThrowsException()
    {
        // Arrange
        var query = new ListSalesQuery(1, 10);

        _saleRepository.GetPaginatedAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Throws(new Exception("Database error"));

        // Act
        var act = () => _handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
    }
}
