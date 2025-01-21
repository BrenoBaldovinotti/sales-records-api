using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// This command captures the required data for creating a sale, 
/// including sale details such as customer email, sale date, branch ID, and items.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateSaleResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateSaleCommandValidator"/>.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    /// <summary>
    /// Gets or sets the customer email for the sale.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of the sale.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the branch ID where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Gets or sets the collection of sale items for this sale.
    /// </summary>
    public List<CreateSaleItemDto> Items { get; set; } = [];

    /// <summary>
    /// Validates the command using the <see cref="CreateSaleCommandValidator"/>.
    /// </summary>
    /// <returns>The validation result containing any validation errors.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
