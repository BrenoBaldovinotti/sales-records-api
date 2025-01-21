using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item within a sale.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the foreign key reference to the Sale entity.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the foreign key reference to the Product entity.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product sold.
    /// Must be greater than zero.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product at the time of the sale.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to this item.
    /// This value is calculated based on business rules.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets or sets the total amount for this item after applying discounts.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the associated sale for this item.
    /// This is a navigation property.
    /// </summary>
    public Sale Sale { get; set; } = null!;

    /// <summary>
    /// Gets or sets the associated product for this item.
    /// This is a navigation property.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Calculates the total amount for this item.
    /// Applies the discount based on quantity and unit price.
    /// <remarks>
    /// If Discount is bigger than the partial total, then returns 0
    /// </remarks>
    /// </summary>
    public void CalculateTotal()
    {
        var partialTotal = UnitPrice * Quantity;

        Total = Discount > partialTotal ? 0 : (partialTotal - Discount);
    }

    /// <summary>
    /// Calculates the discount for this sale item based on its quantity and unit price.
    /// </summary>
    public void CalculateDiscount()
    {
        if (Quantity >= 10)
            Discount = UnitPrice * Quantity * 0.2m; // 20% discount
        else if (Quantity >= 5)
            Discount = UnitPrice * Quantity * 0.1m; // 10% discount
        else
            Discount = 0;
    }
}
