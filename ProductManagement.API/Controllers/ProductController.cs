using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Products.Create;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ICreateProductService _createProductService;
    public ProductController(IProductService productService, ICreateProductService createProductService)
    {
        _productService = productService;
        _createProductService = createProductService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] string? search, decimal?price, string? productType,
    CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(
            search, productType, price);

        return Ok(products);
    }

    [AllowAnonymous]  
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(
    ProductDto dto,
    CancellationToken cancellationToken)
    {
        var result = await _createProductService.CreateAsync(
            dto,
            cancellationToken);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] ProductDto product)
    {
        var result = await _productService.UpdateAsync(id, product);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}