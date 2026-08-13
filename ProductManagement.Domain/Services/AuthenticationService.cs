using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Services;

public class AuthenticationService
{
    public User Authenticate(
        User user,
        bool passwordValid)
    {
        if (!passwordValid)
            throw new InvalidOperationException(
                "Invalid email or password.");

        return user;
    }
}