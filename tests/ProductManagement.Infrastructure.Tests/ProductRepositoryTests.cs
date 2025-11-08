using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Persistence.Repositories;
using Xunit;

namespace ProductManagement.Infrastructure.Tests;

public class ProductRepositoryTests
{
    private ProductManagementDBContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ProductManagementDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ProductManagementDBContext(options);
    }

    [Fact]
    public async Task Should_Add_And_Get_Product()
    {
        var db = CreateInMemoryDb();
        var repo = new ProductRepository(db);

        var product = new Product("Notebook", "Eletrônicos", 3500);
        await repo.AddAsync(product);

        var result = await repo.GetByIdAsync(product.Id);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Notebook");
    }
}