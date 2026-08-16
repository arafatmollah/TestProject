using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using ProductManagement.Application.Orders.Create;
using ProductManagement.Application.Orders.CreateOrder;
using System.Security.Claims;

namespace ProductManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly ICreateOrderService _createOrderService;

    public OrderController(
        ICreateOrderService createOrderService)
    {
        _createOrderService = createOrderService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrderDto dto,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var result = await _createOrderService.CreateAsync(
            dto,
            userId,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok();
    }
}