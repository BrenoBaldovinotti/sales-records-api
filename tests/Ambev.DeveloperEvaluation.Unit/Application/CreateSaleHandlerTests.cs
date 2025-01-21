using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/>.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _productRepository, _branchRepository, _mapper);
    }

    /// <summary>
    /// Successfully creates a sale with valid data.
    /// </summary>
    [Fact(DisplayName = "Successfully create a sale with valid data")]
    public async Task Handle_ValidCommand_CreatesSaleSuccessfully()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var branch = CreateSaleHandlerTestData.GenerateValidBranch();
        var products = command.Items
            .ConvertAll(i => CreateSaleHandlerTestData.GenerateValidProduct(i.ProductId))
;

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(branch);
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(ci =>
            {
                var productId = (Guid)ci.Args()[0];
                return Task.FromResult(products.Find(p => p.Id == productId));
            });

        var sale = CreateSaleHandlerTestData.GenerateSaleFromCommand(command, products);
        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(new CreateSaleResult { Id = Guid.NewGuid() });

        // Mock CreateAsync to return the same Sale object passed to it
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(ci => Task.FromResult((Sale)ci.Args()[0]));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        await _saleRepository.Received(1)
            .CreateAsync(
            Arg.Is<Sale>(s => s.Customer == command.Customer), 
            Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Fails when the branch is not found.
    /// </summary>
    [Fact(DisplayName = "Fail when branch is not found")]
    public async Task Handle_InvalidBranch_ThrowsException()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns((Branch)null!);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Branch not found.");
    }

    /// <summary>
    /// Fails when a product is not found.
    /// </summary>
    [Fact(DisplayName = "Fail when product is not found")]
    public async Task Handle_InvalidProduct_ThrowsException()
    {
        // Arrange
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var branch = CreateSaleHandlerTestData.GenerateValidBranch();

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(branch);
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Product)null!);

        // Mock the first mapping
        var sale = CreateSaleHandlerTestData.GenerateSaleFromCommand(command, new List<Product>());
        _mapper.Map<Sale>(command).Returns(sale);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Product with ID {command.Items[0].ProductId} not found.");
    }


    /// <summary>
    /// Fails for an invalid CreateSaleCommand.
    /// </summary>
    [Fact(DisplayName = "Fail for invalid CreateSaleCommand")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateSaleCommand(); // Invalid: Missing required fields

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>().WithMessage("Validation failed:*");
    }

    /// <summary>
    /// Calculates totals and discounts correctly for valid sale items.
    /// </summary>
    [Fact(DisplayName = "Calculate totals and discounts correctly for sale items")]
    public async Task Handle_ValidCommand_CalculatesTotalsAndDiscountsCorrectly()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            Customer = "customer@example.com",
            SaleDate = new DateTime(2025, 1, 20),
            BranchId = Guid.Parse("61e628b6-64a0-419e-8b33-0e7c76f0a312"),
            Items =
            [
                new CreateSaleItemDto
                {
                    ProductId = Guid.Parse("ad081d1f-46eb-43ae-80ca-37a9b6f4540e"),
                    Quantity = 10
                },
                new CreateSaleItemDto
                {
                    ProductId = Guid.Parse("ca0165b8-c74f-4e8b-8d96-48a7b938c5c9"),
                    Quantity = 5
                }
            ]
        };

        var branch = new Branch
        {
            Id = Guid.Parse("61e628b6-64a0-419e-8b33-0e7c76f0a312"),
            Name = "Test Branch"
        };

        var products = new List<Product>
        {
            new() {
                Id = Guid.Parse("ad081d1f-46eb-43ae-80ca-37a9b6f4540e"),
                BasePrice = 20
            },
            new() {
                Id = Guid.Parse("ca0165b8-c74f-4e8b-8d96-48a7b938c5c9"),
                BasePrice = 50
            }
        };

        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(branch);

        _productRepository.GetByIdAsync(products[0].Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product?>(products[0]));

        _productRepository.GetByIdAsync(products[1].Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product?>(products[1]));
        
        var sale = new Sale
        {
            Customer = command.Customer,
            SaleDate = command.SaleDate,
            BranchId = command.BranchId,
            Items =
            [
                new SaleItem
                {
                    ProductId = command.Items[0].ProductId,
                    Quantity = 10,
                    UnitPrice = 20,
                    Discount = 40,
                    Total = 160
                },
                new SaleItem
                {
                    ProductId = command.Items[1].ProductId,
                    Quantity = 5,
                    UnitPrice = 50,
                    Discount = 25,
                    Total = 225
                }
            ]
        };
        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(new CreateSaleResult { Id = Guid.NewGuid() });

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(ci => Task.FromResult((Sale)ci.Args()[0]));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();

        await _saleRepository.CreateAsync(
            Arg.Is<Sale>(s =>
                s.Items.Count == 2 &&
                s.Items.ElementAt(0).Total == 160 &&
                s.Items.ElementAt(0).Discount == 40 &&
                s.Items.ElementAt(1).Total == 225 &&
                s.Items.ElementAt(1).Discount == 25
            ),
            Arg.Any<CancellationToken>());
    }
}
