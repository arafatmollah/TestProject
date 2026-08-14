using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Products.Create;
using ProductManagement.Application.Products.Delete;
using ProductManagement.Application.Products.Get;
using ProductManagement.Application.Products.GetById;
using ProductManagement.Application.Products.Update;
using ProductManagement.Domain.Entities;

namespace ProductManagement.API.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ICreateProductService _createProductService;
    private readonly IGetProductsService _getProductsService;
    private readonly IGetProductByIdService _getProductByIdService;
    private readonly IUpdateProductService _updateProductService;
    private readonly IDeleteProductService _deleteProductService;
    public ProductController(IProductService productService, ICreateProductService createProductService, IGetProductsService getProductsService, IGetProductByIdService getProductByIdService, IUpdateProductService updateProductService, IDeleteProductService deleteProductService)
    {
        _productService = productService;
        _createProductService = createProductService;
        _getProductsService = getProductsService;
        _getProductByIdService = getProductByIdService;
        _updateProductService = updateProductService;
        _deleteProductService = deleteProductService;

    }

    [AllowAnonymous]
    [HttpGet]
public async Task<IActionResult> GetAll(
    string? search,
    string? productType,
    decimal? price,
    CancellationToken cancellationToken)
{
    var result = await _getProductsService.GetAllAsync(
        search,
        productType,
        price,
        cancellationToken);

    return Ok(result);
}

    //[AllowAnonymous]  
    //[HttpGet("{id:guid}")]
    //public async Task<IActionResult> GetById(Guid id)
    //{
    //    var product = await _productService.GetByIdAsync(id);

    //    if (product == null)
    //        return NotFound();

    //    return Ok(product);
    //}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
    Guid id,
    CancellationToken cancellationToken)
    {
        var result = await _getProductByIdService.GetByIdAsync(
            id,
            cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
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

    //[AllowAnonymous]
    //[HttpPut("{id:guid}")]
    //public async Task<IActionResult> Update(
    //    Guid id,
    //    [FromBody] ProductDto product)
    //{
    //    var result = await _productService.UpdateAsync(id, product);

    //    if (!result)
    //        return NotFound();

    //    return NoContent();
    //}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
    Guid id,
    ProductDto dto,
    CancellationToken cancellationToken)
    {
        var result = await _updateProductService.UpdateAsync(
            id,
            dto,
            cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
    //[HttpDelete("{id:guid}")]
    //public async Task<IActionResult> Delete(Guid id)
    //{
    //    var result = await _productService.DeleteAsync(id);

    //    if (!result)
    //        return NotFound();

    //    return NoContent();
    //}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
    {
        var result = await _deleteProductService.DeleteAsync(
            id,
            cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
}