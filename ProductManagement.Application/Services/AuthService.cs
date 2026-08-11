using Microsoft.AspNetCore.Identity;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDto> RegisterAsync(
        RegisterDto dto)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
            throw new InvalidOperationException(
                "Email already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                dto.Password);

        await _userRepository.AddAsync(user);

        return new AuthResponseDto
        {
            Token = _jwtService.GenerateToken(user)
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(
        LoginDto dto)
    {
        var user =
            await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            return null;

        var result =
            _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        return new AuthResponseDto
        {
            Token = _jwtService.GenerateToken(user)
        };
    }
}