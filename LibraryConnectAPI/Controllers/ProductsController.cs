using LibraryConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryConnectAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products =
    [
        new() { Id = 1, Name = "Laptop", Price = 999.99M },
        new() { Id = 2, Name = "Smartphone", Price = 499.99M }
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll() => Ok(Products);

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        return product is not null ? Ok(product) : NotFound();
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        product.Id = Products.Max(p => p.Id) + 1;
        Products.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product updatedProduct)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return NotFound();

        Products.Remove(product);
        return NoContent();
    }
}