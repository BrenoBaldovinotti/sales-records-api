using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handles the cancellation of a sale.
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventPublisher _eventPublisher;

    public CancelSaleHandler(ISaleRepository saleRepository, IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null) throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");

        sale.Cancel();
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        // Publish the SaleCancelled event
        // Publish SaleCancelled event
        var saleCancelledEvent = new SaleCancelledEvent(sale.Id);
        await _eventPublisher.PublishAsync(saleCancelledEvent, cancellationToken);

        return new CancelSaleResult
        {
            Id = sale.Id,
            Message = $"Sale with ID {sale.Id} has been cancelled."
        };
    }
}
