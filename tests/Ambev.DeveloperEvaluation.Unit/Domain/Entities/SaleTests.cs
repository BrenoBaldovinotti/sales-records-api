using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Ensures the total amount of a sale is correctly calculated based on its items.
    /// </summary>
    [Fact(DisplayName = "RecalculateTotal should correctly compute total sale amount")]
    public void Given_SaleItems_When_RecalculateTotalCalled_Then_TotalAmountShouldBeCorrect()
    {
        // Arrange
        var sale = new Sale();
        sale.Items.Add(new SaleItem { Quantity = 2, UnitPrice = 50, Discount = 10 }); // Total: 90
        sale.Items.Add(new SaleItem { Quantity = 1, UnitPrice = 30, Discount = 5 });  // Total: 25

        // Act
        sale.RecalculateTotal();

        // Assert
        Assert.Equal(115, sale.TotalAmount);
    }

    /// <summary>
    /// Ensures the navigation property SaleItems is initialized.
    /// </summary>
    [Fact(DisplayName = "SaleItems should be initialized")]
    public void Given_NewSale_When_Created_Then_SaleItemsShouldBeEmpty()
    {
        // Arrange
        var sale = new Sale();

        // Assert
        Assert.NotNull(sale.Items);
        Assert.Empty(sale.Items);
    }
}
