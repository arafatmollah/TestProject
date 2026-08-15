using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.Products.Delete;

public class DeleteProductService : IDeleteProductService
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductService(
        IProductRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}