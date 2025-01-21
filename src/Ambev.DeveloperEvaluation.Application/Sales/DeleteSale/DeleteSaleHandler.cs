using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handler for processing DeleteSaleCommand requests.
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of DeleteSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    public DeleteSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the DeleteSaleCommand request.
    /// </summary>
    /// <param name="request">The DeleteSaleCommand.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the sale is not found.</exception>
    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
        {
            return new DeleteSaleResult
            {
                IsSuccessful = false,
                Message = $"Sale with ID {request.Id} not found."
            };
        }

        await _saleRepository.DeleteAsync(sale, cancellationToken);

        return new DeleteSaleResult
        {
            IsSuccessful = true,
            Message = $"Sale with ID {request.Id} was successfully deleted."
        };
    }
}
