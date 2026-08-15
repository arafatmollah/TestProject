namespace ProductManagement.Domain.Exceptions;

public class ProductNotFoundException : DomainException
{
    public ProductNotFoundException(string message)
        : base(message)
    {
    }
}