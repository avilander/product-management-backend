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
    public class GetProductByIdUseCaseTests
    {
        [Fact(DisplayName = "Deve retornar o produto quando ele existir")]
        public async Task ExecuteAsync_Should_Return_Product_When_Found()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product("Mouse Gamer", "Periféricos", 250m);

            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            var useCase = new GetProductByIdUseCase(repoMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("Mouse Gamer");
            result.Category.Should().Be("Periféricos");
            result.UnitCost.Should().Be(250m);

            repoMock.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar null quando o produto não for encontrado")]
        public async Task ExecuteAsync_Should_Return_Null_When_Not_Found()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

            var useCase = new GetProductByIdUseCase(repoMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId, CancellationToken.None);

            // Assert
            result.Should().BeNull();
            repoMock.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
