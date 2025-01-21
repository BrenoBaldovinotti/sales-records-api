using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class GetSaleHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly GetSaleHandler _handler;

    public GetSaleHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Retrieve sale successfully when it exists")]
    public async Task Handle_ValidId_ReturnsSale()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale
        {
            Id = saleId,
            SaleNumber = "12345",
            SaleDate = DateTime.UtcNow,
            Customer = "John Doe",
            BranchId = Guid.NewGuid(),
            TotalAmount = 100.00m,
            Items =
            [
                new SaleItem { ProductId = Guid.NewGuid(), Quantity = 2, UnitPrice = 50.00m, Total = 100.00m }
            ]
        };

        var expectedResult = new GetSaleResult
        {
            Id = saleId,
            SaleNumber = sale.SaleNumber,
            SaleDate = sale.SaleDate,
            Customer = sale.Customer,
            BranchId = sale.BranchId,
            TotalAmount = sale.TotalAmount,
            Items = sale.Items.Select(i => new GetSaleItemResult
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Total = i.Total
            }).ToList()
        };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(new GetSaleQuery(saleId), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        result.Items.Should().HaveCount(1);
        result.TotalAmount.Should().Be(sale.TotalAmount);
    }

    [Fact(DisplayName = "Throw exception when sale is not found")]
    public async Task Handle_InvalidId_ThrowsException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale)null!);

        // Act
        var act = () => _handler.Handle(new GetSaleQuery(saleId), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Sale with ID {saleId} not found.");
    }
}
