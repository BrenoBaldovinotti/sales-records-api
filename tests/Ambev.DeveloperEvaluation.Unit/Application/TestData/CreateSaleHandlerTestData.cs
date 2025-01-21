using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data for CreateSaleHandler tests.
/// </summary>
public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> CreateSaleCommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.Customer, f => f.Internet.Email())
        .RuleFor(s => s.SaleDate, f => f.Date.Past())
        .RuleFor(s => s.BranchId, Guid.NewGuid)
        .RuleFor(s => s.Items, f => CreateSaleItemDtoFaker.Generate(f.Random.Int(1, 5)));

    private static readonly Faker<CreateSaleItemDto> CreateSaleItemDtoFaker = new Faker<CreateSaleItemDto>()
        .RuleFor(i => i.ProductId, Guid.NewGuid)
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 20));

    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Id, Guid.NewGuid)
        .RuleFor(p => p.BasePrice, f => f.Random.Decimal(10, 100));

    private static readonly Faker<Branch> BranchFaker = new Faker<Branch>()
        .RuleFor(b => b.Id, Guid.NewGuid)
        .RuleFor(b => b.Name, f => f.Company.CompanyName());

    /// <summary>
    /// Generates a valid CreateSaleCommand.
    /// </summary>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return CreateSaleCommandFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Branch.
    /// </summary>
    public static Branch GenerateValidBranch()
    {
        return BranchFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Product with a specified ID.
    /// </summary>
    public static Product GenerateValidProduct(Guid productId)
    {
        return ProductFaker.Clone().RuleFor(p => p.Id, _ => productId).Generate();
    }

    /// <summary>
    /// Generates a mocked Sale object based on the provided command.
    /// </summary>
    /// <param name="command">The CreateSaleCommand to base the Sale on.</param>
    /// <param name="products">The list of products used in the sale.</param>
    public static Sale GenerateSaleFromCommand(CreateSaleCommand command, List<Product> products)
    {
        var saleItems = new List<SaleItem>();

        foreach (var item in command.Items)
        {
            var product = products.Find(p => p.Id == item.ProductId);
            saleItems.Add(new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product?.BasePrice ?? 0,
                Discount = 0, // Can be calculated based on quantity
                Total = (product?.BasePrice ?? 0) * item.Quantity // No discount applied here
            });
        }

        return new Sale
        {
            Customer = command.Customer,
            SaleDate = command.SaleDate,
            BranchId = command.BranchId,
            Items = saleItems
        };
    }
}
