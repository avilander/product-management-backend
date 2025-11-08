using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Products.Dtos;
using ProductManagement.Application.Products.UseCases;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using Xunit;

namespace ProductManagement.Application.Tests.Products
{
    public class UpdateProductUseCaseTests
    {
        [Fact(DisplayName = "Deve atualizar o produto existente e retornar o DTO atualizado")]
        public async Task ExecuteAsync_Should_Update_Product_And_Return_Dto()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existing = new Product("Antigo Nome", "Antiga Categoria", 100m);

            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existing);

            var useCase = new UpdateProductUseCase(repoMock.Object);

            var updateDto = new UpdateProductDTO
            {
                Name = "Novo Nome",
                Category = "Nova Categoria",
                UnitCost = 200m
            };

            // Act
            var result = await useCase.ExecuteAsync((productId, updateDto), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Novo Nome");
            result.Category.Should().Be("Nova Categoria");
            result.UnitCost.Should().Be(200m);

            repoMock.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
            repoMock.Verify(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar null quando o produto não existir")]
        public async Task ExecuteAsync_Should_Return_Null_When_Product_Not_Found()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

            var useCase = new UpdateProductUseCase(repoMock.Object);

            var updateDto = new UpdateProductDTO
            {
                Name = "Qualquer",
                Category = "Qualquer",
                UnitCost = 10m
            };

            // Act
            var result = await useCase.ExecuteAsync((productId, updateDto), CancellationToken.None);

            // Assert
            result.Should().BeNull();
            repoMock.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
            repoMock.Verify(r => r.UpdateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
