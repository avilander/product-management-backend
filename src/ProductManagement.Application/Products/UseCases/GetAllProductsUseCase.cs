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
    public class GetAllProductsUseCase : IUseCase<Unit, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDTO>> ExecuteAsync(Unit _, CancellationToken cancellationToken = default)
        {
            var products = await _repository.GetAllAsync(cancellationToken);
            return products.Select(p => (ProductDTO)p).ToList();
        }
    }
}