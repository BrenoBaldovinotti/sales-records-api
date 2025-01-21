using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateSaleValidatorTests
{
    private readonly UpdateSaleValidator _validator;

    public UpdateSaleValidatorTests()
    {
        _validator = new UpdateSaleValidator();
    }

    [Fact(DisplayName = "Valid update sale command passes validation")]
    public void Validate_ValidCommand_ShouldPass()
    {
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            BranchId = Guid.NewGuid(),
            Customer = "Valid Customer",
            Items =
            [
                new UpdateSaleItemCommand { ProductId = Guid.NewGuid(), Quantity = 2 }
            ]
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Missing required fields fails validation")]
    public void Validate_InvalidCommand_ShouldFail()
    {
        // Arrange
        var command = new UpdateSaleCommand
        {
            Id = Guid.Empty, // Invalid ID
            Customer = string.Empty, // Invalid Customer
            BranchId = Guid.Empty, // Invalid Branch ID
            Items = [] // Empty Items
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
        result.ShouldHaveValidationErrorFor(c => c.BranchId);
        result.ShouldHaveValidationErrorFor(c => c.Customer);
        result.ShouldHaveValidationErrorFor(c => c.Items);
    }

}
