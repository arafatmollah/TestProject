namespace ProductManagement.Application.Products.Delete;

public interface IDeleteProductService
{
    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}