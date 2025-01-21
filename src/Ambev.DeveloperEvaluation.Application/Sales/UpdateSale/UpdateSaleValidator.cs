using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand.
/// </summary>
public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleValidator class.
    /// </summary>
    public UpdateSaleValidator()
    {
        RuleFor(s => s.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required.");

        RuleFor(s => s.Customer)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Customer name must not be empty and should be less than 100 characters.");

        RuleFor(s => s.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(s => s.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required.");

        RuleFor(s => s.Items)
            .NotEmpty()
            .WithMessage("Items are required.")
            .ForEach(item =>
            {
                item.ChildRules(i =>
                {
                    i.RuleFor(i => i.ProductId).NotEmpty().WithMessage("Product ID is required.");
                    i.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
                });
            });
    }
}
