using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}