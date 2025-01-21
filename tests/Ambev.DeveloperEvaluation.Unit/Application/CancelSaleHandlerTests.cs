using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Unit tests for <see cref="CancelSaleHandler"/>.
/// </summary>
public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly CancelSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleHandlerTests"/> class.
    /// </summary>
    public CancelSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _eventPublisher = Substitute.For<IEventPublisher>();
        _handler = new CancelSaleHandler(_saleRepository, _eventPublisher);
    }

    /// <summary>
    /// Verifies that a sale is successfully cancelled.
    /// </summary>
    [Fact(DisplayName = "Given valid sale ID When cancelling Then returns success response")]
    public async Task Handle_ValidSaleId_CancelsSaleSuccessfully()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, IsCancelled = false };

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

        // Act
        var result = await _handler.Handle(new CancelSaleCommand(saleId), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(saleId);
        result.Message.Should().Be($"Sale with ID {saleId} has been cancelled.");

        sale.IsCancelled.Should().BeTrue();

        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
        await _eventPublisher.Received(1).PublishAsync(
            Arg.Is<SaleCancelledEvent>(e => e.SaleId == saleId), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Verifies that an exception is thrown if the sale is not found.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale ID When cancelling Then throws KeyNotFoundException")]
    public async Task Handle_InvalidSaleId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var saleId = Guid.NewGuid();

        _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale)null!);

        // Act
        var act = async () => await _handler.Handle(new CancelSaleCommand(saleId), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {saleId} not found.");

        await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
        await _saleRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _eventPublisher.DidNotReceive().PublishAsync(Arg.Any<SaleCancelledEvent>(), Arg.Any<CancellationToken>());
    }
}
