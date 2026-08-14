using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.Products.Delete;

public class DeleteProductService : IDeleteProductService
{
    private readonly IProductRepository _repository;

    public DeleteProductService(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (product == null)
            return false;

        await _repository.DeleteAsync(
            product,
            cancellationToken);

        return true;
    }
}