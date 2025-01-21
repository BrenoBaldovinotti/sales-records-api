using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="branchRepository">The branch repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository, 
        IProductRepository productRepository, 
        IBranchRepository branchRepository, 
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _branchRepository = branchRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if (branch == null) throw new InvalidOperationException("Branch not found.");

        var sale = _mapper.Map<Sale>(command);

        // Generate Sale Number
        sale.SaleNumber = await GenerateUniqueSaleNumberAsync(cancellationToken);

        // Validate products and fetch their prices
        foreach (var item in command.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            if (product == null) throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");

            if (item.Quantity > 20) throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            // Use product's price as unit price
            var saleItem = new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.BasePrice,
                Discount = CalculateDiscount(item.Quantity, product.BasePrice)
            };

            // Calculate the total for the sale item
            saleItem.CalculateTotal();

            sale.Items.Add(saleItem);
        }

        sale.RecalculateTotal();
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }

    /// <summary>
    /// Generates a unique sale number in the format YYYYMMDD-XXXX.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A unique sale number.</returns>
    private async Task<string> GenerateUniqueSaleNumberAsync(CancellationToken cancellationToken)
    {
        string saleNumber;
        do
        {
            // Generate Sale Number: YYYYMMDD-<GUID suffix>
            saleNumber = $"{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8]}";
        } while (await _saleRepository.GetBySaleNumberAsync(saleNumber, cancellationToken) != null);

        return saleNumber;
    }

    /// <summary>
    /// Calculates a discount based on the quantity of items.
    /// </summary>
    /// <param name="quantity">The number of items.</param>
    /// <param name="unitPrice">The unit price of the item.</param>
    /// <returns>The calculated discount.</returns>
    private static decimal CalculateDiscount(int quantity, decimal unitPrice)
    {
        if (quantity >= 10 && quantity <= 20)
            return quantity * unitPrice * 0.2m; // 20% discount
        else if (quantity >= 4 && quantity < 10)
            return quantity * unitPrice * 0.1m; // 10% discount
        return 0m;
    }
}
