using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;
public class GetSaleValidatorTests
{
    private readonly GetSaleValidator _validator;

    public GetSaleValidatorTests()
    {
        _validator = new GetSaleValidator();
    }

    [Fact(DisplayName = "Validation should pass for valid sale ID")]
    public void Given_ValidSaleId_When_Validated_Then_ShouldPass()
    {
        // Arrange
        var command = new GetSaleQuery(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Validation should fail for empty sale ID")]
    public void Given_EmptySaleId_When_Validated_Then_ShouldFail()
    {
        // Arrange
        var command = new GetSaleQuery(Guid.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Sale ID is required.");
    }
}
