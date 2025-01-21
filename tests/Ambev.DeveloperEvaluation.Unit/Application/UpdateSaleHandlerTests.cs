using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IEventPublisher _eventPublish;
    private readonly IMapper _mapper;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _branchRepository = Substitute.For<IBranchRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateSaleHandler(_saleRepository, _productRepository, _branchRepository, _eventPublish, _mapper);
    }

    [Fact(DisplayName = "Given valid sale update When updating Then updates successfully")]
    public async Task Handle_ValidUpdate_UpdatesSuccessfully()
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid(), BranchId = Guid.NewGuid(), Items = new List<SaleItem>() };
        var command = new UpdateSaleCommand
        {
            Id = sale.Id,
            BranchId = sale.BranchId,
            Customer = "Updated Customer",
            Items =
            [
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 5 }
            ]
        };

        _saleRepository.GetByIdAsync(sale.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _branchRepository.GetByIdAsync(sale.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch());
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(new Product { Id = Guid.NewGuid(), BasePrice = 20 });
        _mapper.Map<Sale>(command).Returns(sale);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s => s.Customer == "Updated Customer"), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid branch ID When updating Then throws InvalidOperationException")]
    public async Task Handle_InvalidBranch_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Customer = "Updated Customer",
            Items = []
        };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(new Sale());
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns((Branch)null!);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Branch not found*");
    }

    [Fact(DisplayName = "Given invalid product ID When updating Then throws InvalidOperationException")]
    public async Task Handle_InvalidProduct_ThrowsInvalidOperationException()
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid(), BranchId = Guid.NewGuid() };
        var command = new UpdateSaleCommand
        {
            Id = sale.Id,
            BranchId = sale.BranchId,
            Customer = "Updated Customer",
            Items =
            [
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 5 }
            ]
        };

        _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(new Branch());
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Product)null!);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Product with ID*not found*");
    }
}
