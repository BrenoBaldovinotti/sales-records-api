using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handles the UpdateSaleCommand request.
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IMapper _mapper;

    // <summary>
    /// Initializes a new instance of UpdateSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="productRepository">The product repository.</param>
    /// <param name="branchRepository">The branch repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public UpdateSaleHandler(ISaleRepository saleRepository,
        IProductRepository productRepository,
        IBranchRepository branchRepository,
        IEventPublisher eventPublisher,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _branchRepository = branchRepository;
        _eventPublisher = eventPublisher;
        _mapper = mapper;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        // Validate the command
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var existingSale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingSale == null) throw new InvalidOperationException($"Sale with ID {command.Id} not found.");

        // Validate the branch
        var branch = await _branchRepository.GetByIdAsync(command.BranchId, cancellationToken);
        if (branch == null) throw new InvalidOperationException("Branch not found.");

        // Update sale properties
        existingSale.SaleDate = command.SaleDate;
        existingSale.Customer = command.Customer;
        existingSale.BranchId = command.BranchId;
        existingSale.Items.Clear();

        // Process sale items
        foreach (var item in command.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
            if (product == null) throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");

            if (item.Quantity > 20) throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            var saleItem = new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.BasePrice
            };

            saleItem.CalculateDiscount();
            saleItem.CalculateTotal();
            existingSale.Items.Add(saleItem);
        }

        existingSale.RecalculateTotal();
        var updatedSale = await _saleRepository.UpdateAsync(existingSale, cancellationToken);

        var saleModifiedEvent = new SaleModifiedEvent(existingSale.Id);
        await _eventPublisher.PublishAsync(saleModifiedEvent, cancellationToken);

        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }
}
