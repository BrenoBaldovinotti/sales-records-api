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
    /// - Customer: Required, must be a non-empty string with a maximum length of 100 characters.
    /// - SaleDate: Must not be in the future.
    /// - BranchId: Required and must be a valid GUID.
    /// - Items: 
    ///   - ProductId: Required and must be a valid GUID.
    ///   - Quantity: Must be greater than 0.
    ///   - UnitPrice: Must be greater than 0.
    /// </remarks>
    public CreateSaleCommandValidator()
    {
        RuleFor(s => s.Customer).EmailAddress().NotEmpty().MaximumLength(100);
        RuleFor(s => s.SaleDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");
        RuleFor(s => s.BranchId).NotEmpty();
        RuleForEach(s => s.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId).NotEmpty();
            items.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            items.RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than 0.");
        });
    }
}
