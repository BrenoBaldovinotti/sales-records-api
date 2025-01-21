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

    /// <summary>
    /// Ensures a 20% discount is applied for quantities of 10 or more.
    /// </summary>
    [Fact(DisplayName = "CalculateDiscount should apply 20% discount for quantities of 10 or more")]
    public void Given_Quantity10OrMore_When_CalculateDiscountCalled_Then_Apply20PercentDiscount()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 10,
            UnitPrice = 20 // Expected Discount = 10 * 20 * 0.2 = 40
        };

        // Act
        saleItem.CalculateDiscount();

        // Assert
        Assert.Equal(40, saleItem.Discount);
    }

    /// <summary>
    /// Ensures a 10% discount is applied for quantities between 5 and 9.
    /// </summary>
    [Fact(DisplayName = "CalculateDiscount should apply 10% discount for quantities between 5 and 9")]
    public void Given_QuantityBetween5And9_When_CalculateDiscountCalled_Then_Apply10PercentDiscount()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 7,
            UnitPrice = 15 // Expected Discount = 7 * 15 * 0.1 = 10.5
        };

        // Act
        saleItem.CalculateDiscount();

        // Assert
        Assert.Equal(10.5m, saleItem.Discount);
    }

    /// <summary>
    /// Ensures no discount is applied for quantities less than 5.
    /// </summary>
    [Fact(DisplayName = "CalculateDiscount should apply no discount for quantities less than 5")]
    public void Given_QuantityLessThan5_When_CalculateDiscountCalled_Then_ApplyNoDiscount()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = 3,
            UnitPrice = 50 // Expected Discount = 0
        };

        // Act
        saleItem.CalculateDiscount();

        // Assert
        Assert.Equal(0, saleItem.Discount);
    }
}
