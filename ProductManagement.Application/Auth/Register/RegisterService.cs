using Microsoft.AspNetCore.Identity;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.Auth.Register;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly RegistrationService _registrationService;

    public RegisterService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        RegistrationService registrationService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _registrationService = registrationService;
    }

    public async Task<RegisterResponseDto> RegisterAsync(
        RegisterDto dto,
        CancellationToken cancellationToken = default)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(
                dto.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "Email already exists.");
        }

        var temporaryUser = new User
        {
            Email = dto.Email
        };

        var passwordHash =
            _passwordHasher.HashPassword(
                temporaryUser,
                dto.Password);

        var user = _registrationService.Register(
            dto.Email,
            passwordHash);

        await _userRepository.AddAsync(
            user,
            cancellationToken);

        return new RegisterResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}