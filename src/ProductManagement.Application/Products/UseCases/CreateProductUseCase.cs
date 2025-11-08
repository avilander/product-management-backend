using MassTransit;
using ProductManagement.Application.Abstractions;
using ProductManagement.Application.Products.Dtos;
using ProductManagement.Application.Products.Events;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.UseCases.Impl
{

    public class CreateProductUseCase : IUseCase<CreateProductDTO, ProductDTO>
    {

        private readonly IProductRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;


        public CreateProductUseCase(IProductRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<ProductDTO> ExecuteAsync(CreateProductDTO request, CancellationToken cancellationToken = default)
        {
            var product = new Product(request.Name, request.Category, request.UnitCost);
            ProductDTO productDTO = product;
            await _repository.AddAsync(product, cancellationToken);

            await PublishMessage(product, cancellationToken);
            
            return productDTO;
        }

        private async Task PublishMessage(Product product, CancellationToken cancellationToken = default) 
        {
            // publica evento na fila
            await _publishEndpoint.Publish<IProductEvent>(new
            {
                ProductId = product.Id,
                EventType = "Created",
                Name = product.Name,
                Category = product.Category,
                UnitCost = product.UnitCost,
                OccurredAt = DateTime.UtcNow
            }, cancellationToken);
        }
    }
}
