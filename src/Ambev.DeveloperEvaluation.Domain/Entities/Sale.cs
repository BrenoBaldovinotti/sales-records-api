using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale record in the system.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique sale number.
    /// This is used to identify the sale externally.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer name associated with the sale.
    /// Must not be null or empty.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch where the sale was made.
    /// This is a foreign key reference to the Branch entity.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// This value is the sum of all item totals in the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the cancellation status of the sale.
    /// Indicates whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the associated branch for this sale.
    /// This is a navigation property.
    /// </summary>
    public Branch Branch { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of items included in the sale.
    /// This is a navigation property.
    /// </summary>
    public ICollection<SaleItem> Items { get; set; } = [];

    /// <summary>
    /// Cancels the sale and updates the cancellation status.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }

    /// <summary>
    /// Recalculates the total amount of the sale based on its items.
    /// </summary>
    public void RecalculateTotal()
    {
        foreach (var item in Items)
        {
            item.CalculateTotal();
        }

        TotalAmount = Items.Sum(item => item.Total);
    }
}
