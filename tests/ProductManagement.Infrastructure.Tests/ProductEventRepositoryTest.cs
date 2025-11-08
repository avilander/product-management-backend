using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enum;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagement.Infrastructure.Tests
{
    public class ProductEventRepositoryTests
    {
        private ProductManagementDBContext CreateInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ProductManagementDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // banco isolado por teste
                .Options;

            return new ProductManagementDBContext(options);
        }

        [Fact(DisplayName = "Deve salvar um ProductEvent no banco")]
        public async Task AddAsync_Should_Persist_Event()
        {
            // Arrange
            var db = CreateInMemoryDb();
            var repo = new ProductEventRepository(db);

            var productId = Guid.NewGuid();
            var ev = new ProductEvent(
                productId,
                ProductEventType.Created,
                "Produto Teste",
                "Categoria",
                100m
            );

            // Act
            await repo.AddAsync(ev, CancellationToken.None);

            // Assert
            var saved = await db.ProductEvents.FirstOrDefaultAsync();
            saved.Should().NotBeNull();
            saved!.ProductId.Should().Be(productId);
            saved.EventType.Should().Be(ProductEventType.Created);
            saved.Name.Should().Be("Produto Teste");
        }

    }
}
