using ProductManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ProductType.DeleteProductType
{
    public class DeleteProductTypeService : IDeleteProductType
    {
        private readonly IProductTypeRepository _repository;
        public DeleteProductTypeService(IProductTypeRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var productType = await _repository.GetByIdAsync(
            id);

            if (productType == null)
                return false;

            await _repository.DeleteAsync(
                id);

            return true;
        }
    }
}
