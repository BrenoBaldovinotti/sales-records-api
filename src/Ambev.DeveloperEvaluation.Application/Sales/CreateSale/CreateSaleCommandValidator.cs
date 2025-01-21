using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for creating a sale.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - **Customer**: Must be a valid email address, non-empty, and have a maximum length of 100 characters.
    /// - **SaleDate**: Cannot be in the future.
    /// - **BranchId**: Must not be empty and must be a valid GUID.
    /// - **Items**:
    ///   - Must not be empty (at least one item required).
    ///   - **ProductId**: Required and must be a valid GUID.
    ///   - **Quantity**: Must be greater than 0.
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        // Validate Customer field
        RuleFor(s => s.Customer)
            .NotEmpty().WithMessage("Customer is required.")
            .MaximumLength(100).WithMessage("Customer must not exceed 100 characters.")
            .EmailAddress().WithMessage("Customer must be a valid email address.");

        // Validate SaleDate field
        RuleFor(s => s.SaleDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future.");

        // Validate BranchId field
        RuleFor(s => s.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required.");

        // Validate Items collection
        RuleFor(s => s.Items)
            .NotEmpty()
            .WithMessage("At least one sale item is required.");

        // Validate each item in the Items collection
        RuleForEach(s => s.Items).ChildRules(items =>
        {
            // Validate ProductId field
            items.RuleFor(i => i.ProductId)
                .NotEmpty()
                .WithMessage("Product ID is required.");

            // Validate Quantity field
            items.RuleFor(i => i.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");
        });
    }
}
