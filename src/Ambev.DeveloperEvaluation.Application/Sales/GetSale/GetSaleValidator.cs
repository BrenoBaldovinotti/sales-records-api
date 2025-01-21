using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Validator for GetSaleByIdCommand.
/// </summary>
public class GetSaleValidator : AbstractValidator<GetSaleQuery>
{
    /// <summary>
    /// Initializes validation rules for GetSaleByIdCommand.
    /// </summary>
    public GetSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required.");
    }
}
