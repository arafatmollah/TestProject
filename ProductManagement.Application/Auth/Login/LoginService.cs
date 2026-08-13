using Microsoft.AspNetCore.Identity;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.Auth.Login;

public class LoginService : ILogInService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly AuthenticationService _authenticationService;

    public LoginService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtService jwtService,
        AuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _authenticationService = authenticationService;
    }

    public async Task<LogInResponseDto> LoginAsync(
        LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(
            dto.Email);

        if (user == null)
        {
            throw new InvalidOperationException(
                "Invalid email or password.");
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password);

        var passwordValid =
            result != PasswordVerificationResult.Failed;

        _authenticationService.Authenticate(
            user,
            passwordValid);

        var token = _jwtService.GenerateToken(user);

        return new LogInResponseDto
        {
            Email = user.Email,
            Token = token
        };
    }
}