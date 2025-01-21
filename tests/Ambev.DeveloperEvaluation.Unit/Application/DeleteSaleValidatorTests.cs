using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteSaleValidatorTests
{
    private readonly DeleteSaleValidator _validator;

    public DeleteSaleValidatorTests()
    {
        _validator = new DeleteSaleValidator();
    }

    [Fact(DisplayName = "Valid sale ID passes validation")]
    public void Validate_ValidSaleId_ShouldPass()
    {
        var command = new DeleteSaleCommand { Id = Guid.NewGuid() };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    [Fact(DisplayName = "Empty sale ID fails validation")]
    public void Validate_EmptySaleId_ShouldFail()
    {
        var command = new DeleteSaleCommand { Id = Guid.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }
}
