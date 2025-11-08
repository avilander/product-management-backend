using ProductManagement.Application.Abstractions;
using ProductManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.UseCases
{
    public class DeleteProductUseCase : IUseCase<Guid, bool>
    {
        private readonly IProductRepository _repository;

        public DeleteProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                return false;

            await _repository.DeleteAsync(product, cancellationToken);
            return true;
        }
    }
}
