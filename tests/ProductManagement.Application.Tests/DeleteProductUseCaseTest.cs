using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Products.UseCases;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using Xunit;

namespace ProductManagement.Application.Tests.Products
{
    public class DeleteProductUseCaseTests
    {
        [Fact(DisplayName = "Deve deletar o produto existente e retornar true")]
        public async Task ExecuteAsync_Should_Delete_Existing_Product_And_Return_True()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product("Mouse Gamer", "Periféricos", 250m);

            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            var useCase = new DeleteProductUseCase(repoMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId, CancellationToken.None);

            // Assert
            result.Should().BeTrue("porque o produto existe e deve ser deletado");
            repoMock.Verify(r => r.GetByIdAsync(productId, It.IsAny<CancellationToken>()), Times.Once);
            repoMock.Verify(r => r.DeleteAsync(product, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar false quando o produto não for encontrado")]
        public async Task ExecuteAsync_Should_Return_False_When_Product_Not_Found()
        {
            // Arrange
            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

            var useCase = new DeleteProductUseCase(repoMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            result.Should().BeFalse("porque o produto não existe");
            repoMock.Verify(r => r.DeleteAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}