using Moq;
using ProductManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MassTransit;
using ProductManagement.Application.Products.UseCases.Impl;
using ProductManagement.Application.Products.Dtos;
using FluentAssertions;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Tests;

public class CreateProductUseCaseTests
{
    [Fact]
    public async Task Should_Create_Product_And_Publish_Event()
    {
        // Arrange
        var repoMock = new Mock<IProductRepository>();
        var publishMock = new Mock<IPublishEndpoint>();

        var useCase = new CreateProductUseCase(repoMock.Object, publishMock.Object);
        var dto = new CreateProductDTO { Name = "Teclado", Category = "Periférico", UnitCost = 250 };

        // Act
        var result = await useCase.ExecuteAsync(dto);

        // Assert
        result.Name.Should().Be("Teclado");
        repoMock.Verify(r => r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreateProductAndPublishEvent()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        var publishMock = new Mock<IPublishEndpoint>();

        var useCase = new CreateProductUseCase(
            repositoryMock.Object,
            publishMock.Object);

        var dto = new CreateProductDTO
        {
            Name = "Produto Teste",
            Category = "Cat",
            UnitCost = 50m
        };

        // Act
        var result = await useCase.ExecuteAsync(dto, CancellationToken.None);

        // Assert – produto retornado
        result.Name.Should().Be(dto.Name);
        result.Category.Should().Be(dto.Category);
        result.UnitCost.Should().Be(dto.UnitCost);

        // Assert – repositório chamado
        repositoryMock.Verify(r =>
            r.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}