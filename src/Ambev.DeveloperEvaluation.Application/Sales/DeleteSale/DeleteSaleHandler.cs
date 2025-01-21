using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handles the deletion of a sale.
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, Unit>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes the handler with the required dependencies.
    /// </summary>
    /// <param name="saleRepository">The repository for sales operations.</param>
    public DeleteSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the command to delete a sale.
    /// </summary>
    /// <param name="request">The command containing the sale ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>An empty result upon successful deletion.</returns>
    public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null) throw new InvalidOperationException($"Sale with ID {request.Id} not found.");

        await _saleRepository.DeleteAsync(sale, cancellationToken);
        return Unit.Value;
    }
}
