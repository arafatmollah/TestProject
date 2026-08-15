using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Products.Get;
using ProductManagement.Application.ProductType.CreateProductType;
using ProductManagement.Application.ProductType.GetByIdProductType;
using ProductManagement.Application.ProductType.GetProductType;
using ProductManagement.Application.Services;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;
        private readonly ICreateProductType _createProductTypeService;
        private readonly IGetProductType _getProductType;
        private readonly IGetProductTypeById _getProductTypeById;
        public ProductTypeController(IProductTypeService productTypeService, ICreateProductType createProductTypeService, IGetProductType getProductType, IGetProductTypeById getProductTypeById)
        {
            _productTypeService = productTypeService;
            _createProductTypeService = createProductTypeService;
            _getProductType = getProductType;
            _getProductTypeById = getProductTypeById;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productType = await _getProductType.GetAllAsync();

            return Ok(productType);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productType = await _getProductTypeById.GetByIdAsync(id);

            if (productType == null)
                return NotFound();

            return Ok(productType);
        }
        [HttpPost]
        public async Task<IActionResult> Create(
        [FromBody] ProductTypeDto productType)
        {
            var result = await _createProductTypeService.CreateAsync(productType);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
       Guid id,
       [FromBody] ProductTypeDto product)
        {
            var result = await _productTypeService.UpdateAsync(id, product);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productTypeService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
