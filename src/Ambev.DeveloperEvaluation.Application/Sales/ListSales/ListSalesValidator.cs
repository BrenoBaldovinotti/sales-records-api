using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

/// <summary>
/// Validator for ListSaleCommand.
/// </summary>
public class ListSalesValidator : AbstractValidator<ListSalesQuery>
{
    /// <summary>
    /// Initializes validation rules for ListSaleCommand.
    /// </summary>
    public ListSalesValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}
