using LibraryConnectAPI.Controllers;
using LibraryConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryConnectAPI.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly ProductsController _controller = new();

    [Fact]
    public void GetAll_ReturnsListOfProducts()
    {
        // Act
        var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
        Assert.NotEmpty(products);
    }

    [Fact]
    public void GetById_ReturnsProduct_WhenExists()
    {
        // Act
        var result = _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var product = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(1, product.Id);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenNotExists()
    {
        // Act
        var result = _controller.GetById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void Create_AddsProduct_AndReturnsCreated()
    {
        // Arrange
        var newProduct = new Product { Name = "Tablet", Price = 299.99M };

        // Act
        var result = _controller.Create(newProduct);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var product = Assert.IsType<Product>(createdResult.Value);
        Assert.Equal("Tablet", product.Name);
        Assert.True(product.Id > 2);
    }

    [Fact]
    public void Delete_RemovesProduct_WhenExists()
    {
        // Act
        var result = _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNotFound_WhenNotExists()
    {
        // Act
        var result = _controller.Delete(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}