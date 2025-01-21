using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating a sale.
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid Id { get; set; }
    public string Customer { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public Guid BranchId { get; set; }
    public List<UpdateSaleItemCommand> Items { get; set; } = [];
}

/// <summary>
/// Command for updating a sale item.
/// </summary>
public class UpdateSaleItemCommand
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
