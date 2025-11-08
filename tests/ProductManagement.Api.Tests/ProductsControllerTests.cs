using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.Api.Controllers;
using ProductManagement.Application.Abstractions;
using ProductManagement.Application.Products.Dtos;
using Xunit;

namespace ProductManagement.Api.Tests;

public class ProductsControllerTests
{
    private readonly Mock<IUseCase<Unit, IEnumerable<ProductDTO>>> _getAllMock = new();
    private readonly Mock<IUseCase<Guid, ProductDTO?>> _getByIdMock = new();
    private readonly Mock<IUseCase<CreateProductDTO, ProductDTO>> _createMock = new();
    private readonly Mock<IUseCase<(Guid id, UpdateProductDTO dto), ProductDTO>> _updateMock = new();
    private readonly Mock<IUseCase<Guid, bool>> _deleteMock = new();

    private ProductsController CreateController()
        => new(
            _getAllMock.Object,
            _getByIdMock.Object,
            _createMock.Object,
            _updateMock.Object,
            _deleteMock.Object);

    [Fact]
    public async Task GetAll_ShouldReturnOkWithProducts()
    {
        // Arrange
        var products = new List<ProductDTO>
        {
            new(Guid.NewGuid(), "Prod 1", "Cat", 10m, DateTime.UtcNow)
        };

        _getAllMock
            .Setup(x => x.ExecuteAsync(Unit.Value, It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        var controller = CreateController();

        // Act
        var result = await controller.GetAll(CancellationToken.None);

        // Assert
        var ok = result.Result as OkObjectResult;
        ok.Should().NotBeNull();
        ok!.Value.Should().BeEquivalentTo(products);
    }

    [Fact]
    public async Task GetById_WhenNotFound_ShouldReturnNotFound()
    {
        // Arrange
        _getByIdMock
            .Setup(x => x.ExecuteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductDTO?)null);

        var controller = CreateController();

        // Act
        var result = await controller.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAt()
    {
        // Arrange
        var dto = new CreateProductDTO { Name = "Prod", Category = "Cat", UnitCost = 20m };
        var created = new ProductDTO(Guid.NewGuid(), dto.Name, dto.Category, dto.UnitCost, DateTime.UtcNow);

        _createMock
            .Setup(x => x.ExecuteAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(created);

        var controller = CreateController();

        // Act
        var result = await controller.Create(dto, CancellationToken.None);

        // Assert
        var createdAt = result.Result as CreatedAtActionResult;
        createdAt.Should().NotBeNull();
        createdAt!.Value.Should().BeEquivalentTo(created);
    }
}
