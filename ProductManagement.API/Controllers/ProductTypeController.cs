using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Services;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productType = await _productTypeService.GetAllAsync();

            return Ok(productType);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var productType = await _productTypeService.GetByIdAsync(id);

            if (productType == null)
                return NotFound();

            return Ok(productType);
        }
        [HttpPost]
        public async Task<IActionResult> Create(
        [FromBody] ProductTypeDto productType)
        {
            var result = await _productTypeService.CreateAsync(productType);

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
