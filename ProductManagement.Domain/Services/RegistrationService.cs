using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Domain.Services;

public class RegistrationService
{
    public User Register(
        string firstName,
        string lastName,
        string phone,
        string email,
        string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new BusinessRuleException(
                "First name is required.");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new BusinessRuleException(
                "Last name is required.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessRuleException(
                "Email is required.");
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new BusinessRuleException(
                "Password hash is required.");
        }

        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Phone = phone.Trim(),
            Email = email.Trim(),
            PasswordHash = passwordHash,
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };
    }
}