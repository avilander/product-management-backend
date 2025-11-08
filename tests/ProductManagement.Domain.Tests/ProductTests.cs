using FluentAssertions;
using ProductManagement.Domain.Entities;
using Xunit;

namespace ProductManagement.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void Should_Create_Product_With_Valid_Data()
    {
        // Act
        var product = new Product("Mouse Gamer", "Peripherals", 199.90m);

        // Assert
        product.Name.Should().Be("Mouse Gamer");
        product.Category.Should().Be("Peripherals");
        product.UnitCost.Should().Be(199.90m);
        product.CreatedAt.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
    }

    [Fact]
    public void Should_Throw_When_Name_Is_Empty()
    {
        Action act = () => new Product("", "Category", 10);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Name*");
    }
}