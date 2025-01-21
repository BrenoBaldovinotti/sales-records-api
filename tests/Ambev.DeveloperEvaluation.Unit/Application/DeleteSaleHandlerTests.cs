using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleHandler(_saleRepository);
    }

    [Fact(DisplayName = "Given valid sale ID When deleting Then deletes successfully")]
    public async Task Handle_ValidSaleId_DeletesSuccessfully()
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid() };
        _saleRepository.GetByIdAsync(sale.Id, Arg.Any<CancellationToken>()).Returns(sale);

        // Act
        await _handler.Handle(new DeleteSaleCommand { Id = sale.Id }, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).DeleteAsync(sale, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale ID When deleting Then throws InvalidOperationException")]
    public async Task Handle_InvalidSaleId_ThrowsInvalidOperationException()
    {
        // Arrange
        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Sale)null!);

        // Act
        var act = () => _handler.Handle(new DeleteSaleCommand { Id = Guid.NewGuid() }, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*not found*");
    }
}
