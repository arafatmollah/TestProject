using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Auth.Login;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRegisterService _registerService;
    private readonly ILogInService _loginService;

    public AuthController(IRegisterService registerService, ILogInService logInService)
    {
       
        _registerService = registerService;
        _loginService = logInService;
    }

    
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _registerService.RegisterAsync(
                dto,
                cancellationToken);

            return Ok(result);
        }
        catch(InvalidProgramException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        var result = await _loginService.LoginAsync(dto);
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