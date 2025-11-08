using ProductManagement.Application.Abstractions;
using ProductManagement.Application.Products.Dtos;
using ProductManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.UseCases
{
    public class GetProductByIdUseCase : IUseCase<Guid, ProductDTO?>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDTO?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                return null;

            return product;
        }
    }
}