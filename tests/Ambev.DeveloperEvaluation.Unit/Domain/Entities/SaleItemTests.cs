using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the SaleItem entity.
/// </summary>
public class SaleItemTests
{
    /// <summary>
    /// Ensures the total is correctly calculated for a sale item.
    /// </summary>
    [Fact(DisplayName = "CalculateTotal should correctly compute total for sale item")]
    public void Given_UnitPriceAndQuantity_When_CalculateTotalCalled_Then_TotalShouldBeCorrect()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 3,
            UnitPrice = 20,
            Discount = 10 // Total = (3 * 20) - 10 = 50
        };

        // Act
        saleItem.CalculateTotal();

        // Assert
        Assert.Equal(50, saleItem.Total);
    }

    /// <summary>
    /// Ensures the total does not go below zero due to invalid discounts.
    /// </summary>
    [Fact(DisplayName = "CalculateTotal should prevent negative totals")]
    public void Given_InvalidDiscount_When_CalculateTotalCalled_Then_TotalShouldNotBeNegative()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 1,
            UnitPrice = 10,
            Discount = 15 // Invalid: discount exceeds total
        };

        // Act
        saleItem.CalculateTotal();

        // Assert
        Assert.Equal(0, saleItem.Total); // Total should not be negative
    }
}
