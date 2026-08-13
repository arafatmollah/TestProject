using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IRegisterService _registerService;

    public AuthController(IAuthService authService, IRegisterService registerService)
    {
        _authService = authService;
        _registerService = registerService;
    }

    //[HttpPost("register")]
    //public async Task<IActionResult> Register(
    //    RegisterDto dto)
    //{
    //    try
    //    {
    //        var result =
    //            await _authService.RegisterAsync(dto);

    //        return Ok(result);
    //    }
    //    catch (InvalidOperationException ex)
    //    {
    //        return BadRequest(new
    //        {
    //            message = ex.Message
    //        });
    //    }
    //}
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _registerService.RegisterAsync(
            dto,
            cancellationToken);

        return Ok(result);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        var result =
            await _authService.LoginAsync(dto);

        if (result == null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        return Ok(result);
    }
}