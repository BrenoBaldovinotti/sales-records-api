using Ambev.DeveloperEvaluation.Common.Models;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sales entity operations
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Creates a new sale in the database
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all sales from the database
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of sales</returns>
    Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Ùpdate a sale from the database
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale entity.
    /// </summary>
    /// <param name="sale">The sale to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the operation.</returns>
    Task DeleteAsync(Sale sale, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a sale by its unique sale number.
    /// </summary>
    /// <param name="saleNumber">The unique sale number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sale if found, null otherwise.</returns>
    Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of sales from the database.
    /// </summary>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of sales per page.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A paginated list of sales.</returns>
    Task<PaginatedListModel<Sale>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
