using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for the UpdateSaleRequest.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes validation rules for UpdateSaleRequest.
    /// </summary>
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required.");

        RuleFor(sale => sale.Customer)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Customer name must not be empty and should be less than 100 characters.");

        RuleFor(sale => sale.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required.");

        RuleForEach(sale => sale.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId).NotEmpty().WithMessage("Product ID is required.");
            items.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        });
    }
}
