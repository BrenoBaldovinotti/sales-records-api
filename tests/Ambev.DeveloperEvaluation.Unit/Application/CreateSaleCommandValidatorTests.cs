using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation.TestHelper;
using Xunit;
using System;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleCommandValidator"/>.
/// </summary>
public class CreateSaleCommandValidatorTests
{
    private readonly CreateSaleCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleCommandValidatorTests"/> class.
    /// </summary>
    public CreateSaleCommandValidatorTests()
    {
        _validator = new CreateSaleCommandValidator();
    }

    /// <summary>
    /// Tests that validation passes for a valid CreateSaleCommand.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for a valid CreateSaleCommand")]
    public void Given_ValidCommand_When_Validated_Then_ShouldPass()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Customer = "test@example.com",
            SaleDate = DateTime.UtcNow.AddHours(-1), // Ensure it's not "in the future"
            BranchId = Guid.NewGuid(),
            Items =
            [
                new CreateSaleItemDto
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5
                }
            ]
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when required fields are missing.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when required fields are missing")]
    public void Given_MissingFields_When_Validated_Then_ShouldFail()
    {
        // Arrange
        var command = new CreateSaleCommand(); // Empty command

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Customer)
              .WithErrorMessage("Customer is required.");
        result.ShouldHaveValidationErrorFor(c => c.BranchId)
              .WithErrorMessage("Branch ID is required.");
        result.ShouldHaveValidationErrorFor(c => c.Items)
              .WithErrorMessage("At least one sale item is required.");
    }

    /// <summary>
    /// Tests that validation fails when SaleDate is in the future.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for future SaleDate")]
    public void Given_FutureSaleDate_When_Validated_Then_ShouldFail()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Customer = "test@example.com",
            SaleDate = DateTime.UtcNow.AddDays(1), // Future date
            BranchId = Guid.NewGuid(),
            Items = new List<CreateSaleItemDto>
            {
                new CreateSaleItemDto
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5
                }
            }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.SaleDate)
              .WithErrorMessage("Sale date cannot be in the future.");
    }

    /// <summary>
    /// Tests that validation fails when Items collection is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for empty Items collection")]
    public void Given_EmptyItems_When_Validated_Then_ShouldFail()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Customer = "test@example.com",
            SaleDate = DateTime.UtcNow,
            BranchId = Guid.NewGuid(),
            Items = new List<CreateSaleItemDto>() // Empty items list
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Items)
              .WithErrorMessage("At least one sale item is required.");
    }

    /// <summary>
    /// Tests that validation fails for invalid ProductId and Quantity in Items.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid ProductId and Quantity in Items")]
    public void Given_InvalidItems_When_Validated_Then_ShouldFail()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Customer = "test@example.com",
            SaleDate = DateTime.UtcNow,
            BranchId = Guid.NewGuid(),
            Items = new List<CreateSaleItemDto>
            {
                new CreateSaleItemDto
                {
                    ProductId = Guid.Empty, // Invalid ProductId
                    Quantity = 0 // Invalid Quantity
                }
            }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor("Items[0].ProductId")
              .WithErrorMessage("Product ID is required.");
        result.ShouldHaveValidationErrorFor("Items[0].Quantity")
              .WithErrorMessage("Quantity must be greater than 0.");
    }

    /// <summary>
    /// Tests that validation fails for Customer exceeding maximum length.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for Customer exceeding max length")]
    public void Given_LongCustomerName_When_Validated_Then_ShouldFail()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Customer = new string('a', 101), // 101 characters
            SaleDate = DateTime.UtcNow,
            BranchId = Guid.NewGuid(),
            Items = new List<CreateSaleItemDto>
            {
                new CreateSaleItemDto
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5
                }
            }
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Customer)
              .WithErrorMessage("Customer must not exceed 100 characters.");
    }
}
