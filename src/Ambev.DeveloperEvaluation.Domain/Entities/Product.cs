using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product available for sale.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base price of the product.
    /// This value is used for calculating totals in sales.
    /// </summary>
    public decimal BasePrice { get; set; }

    /// <summary>
    /// Gets or sets the collection of sale items associated with this product.
    /// This is a navigation property.
    /// </summary>
    public ICollection<SaleItem> SaleItems { get; set; } = [];
}
