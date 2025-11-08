using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ProductManagement.Application.Abstractions;
using ProductManagement.Application.Products.Dtos;
using ProductManagement.Application.Products.UseCases;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using Xunit;

namespace ProductManagement.Application.Tests.Products
{
    public class GetAllProductsUseCaseTests
    {
        [Fact(DisplayName = "Deve retornar todos os produtos")]
        public async Task ExecuteAsync_Should_Return_All_Products()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product("Mouse Gamer", "Periféricos", 250m),
                new Product("Teclado Mecânico", "Periféricos", 450m)
            };

            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            var useCase = new GetAllProductsUseCase(repoMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(Unit.Value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            var list = new List<ProductDTO>(result);

            list[0].Name.Should().Be("Mouse Gamer");
            list[0].Category.Should().Be("Periféricos");
            list[0].UnitCost.Should().Be(250m);

            list[1].Name.Should().Be("Teclado Mecânico");
            list[1].Category.Should().Be("Periféricos");
            list[1].UnitCost.Should().Be(450m);

            repoMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar lista vazia quando não houver produtos")]
        public async Task ExecuteAsync_Should_Return_Empty_When_No_Products()
        {
            // Arrange
            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Product>());

            var useCase = new GetAllProductsUseCase(repoMock.Object);

            // Act
            var result = await useCase.ExecuteAsync(Unit.Value, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
            repoMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
