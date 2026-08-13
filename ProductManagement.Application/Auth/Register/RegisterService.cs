using FluentValidation;
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
    private readonly IValidator<RegisterDto> _validator;

    public RegisterService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        RegistrationService registrationService, IValidator<RegisterDto> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _registrationService = registrationService;
        _validator = validator;
    }

    public async Task<RegisterResponseDto> RegisterAsync(
     RegisterDto dto,
     CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(
            dto,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(
                validationResult.Errors);
        }

        var existingUser =
            await _userRepository.GetByEmailAsync(
                dto.Email,
                cancellationToken);

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
            dto.FirstName,
            dto.LastName,
            dto.Phone,
            dto.Email,
            passwordHash);

        await _userRepository.AddAsync(
            user,
            cancellationToken);

        return new RegisterResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}