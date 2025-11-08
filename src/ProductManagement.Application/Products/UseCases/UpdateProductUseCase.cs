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
    public class UpdateProductUseCase : IUseCase<(Guid id, UpdateProductDTO dto), ProductDTO?>
    {
        private readonly IProductRepository _repository;

        public UpdateProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDTO?> ExecuteAsync((Guid id, UpdateProductDTO dto) request, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByIdAsync(request.id, cancellationToken);
            if (product is null)
                return null;

            if(!string.IsNullOrEmpty(request.dto.Name) ) product.SetName(request.dto.Name);
            if(!string.IsNullOrEmpty(request.dto.Category)) product.SetCategory(request.dto.Category);
            if (request.dto.UnitCost > 0) product.SetUnitCost(request.dto.UnitCost);

            await _repository.UpdateAsync(product, cancellationToken);

            return product;
        }
    }
}
