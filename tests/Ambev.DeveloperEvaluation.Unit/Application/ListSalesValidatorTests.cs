using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Unit tests for the <see cref="ListSalesQueryValidator"/> class.
/// </summary>
public class ListSalesValidatorTests
{
    private readonly ListSalesValidator _validator;

    /// <summary>
    /// Initializes a new instance of <see cref="ListSalesQueryValidatorTests"/>.
    /// </summary>
    public ListSalesValidatorTests()
    {
        _validator = new ListSalesValidator();
    }

    /// <summary>
    /// Tests that a valid query passes validation.
    /// </summary>
    [Fact(DisplayName = "Validate valid query")]
    public void Given_ValidQuery_When_Validated_ShouldPass()
    {
        // Arrange
        var query = new ListSalesQuery(1, 10);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when page number is invalid.
    /// </summary>
    [Fact(DisplayName = "Validate invalid page number")]
    public void Given_InvalidPageNumber_When_Validated_ShouldFail()
    {
        // Arrange
        var query = new ListSalesQuery(0, 10);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.PageNumber)
            .WithErrorMessage("Page number must be greater than 0.");
    }

    /// <summary>
    /// Tests that validation fails when page size is invalid.
    /// </summary>
    [Fact(DisplayName = "Validate invalid page size")]
    public void Given_InvalidPageSize_When_Validated_ShouldFail()
    {
        // Arrange
        var query = new ListSalesQuery(1, 101);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(q => q.PageSize)
            .WithErrorMessage("Page size must be between 1 and 100.");
    }
}
