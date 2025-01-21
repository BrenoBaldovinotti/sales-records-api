using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Sale>? Sales { get; set; }
    public DbSet<SaleItem>? SaleItems { get; set; }
    public DbSet<Branch>? Branches { get; set; }
    public DbSet<Product>? Products { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all configurations from the current assembly (i.g. Adapters/Driven/Infrastructure/ORM/Mapping)
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Seed data for products
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product A",
                BasePrice = 50.00m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product B",
                BasePrice = 30.00m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product C",
                BasePrice = 20.00m
            }
        );

        // Seed data for branches
        modelBuilder.Entity<Branch>().HasData(
            new Branch
            {
                Id = Guid.NewGuid(),
                Name = "Branch 1",
            },
            new Branch
            {
                Id = Guid.NewGuid(),
                Name = "Branch 2",
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
        );

        return new DefaultContext(builder.Options);
    }
}
