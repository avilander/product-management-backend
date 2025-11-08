
using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using ProductManagement.Application.Products.Events;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enum;
using ProductManagement.Domain.Repositories;
using ProductManagement.Infrastructure.Persistence;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProductManagement.Infrastructure.Messaging
{
    public class ProductEventConsumer : IConsumer<IProductEvent>
    {
        private readonly IProductEventRepository _productEventRepository;

        public ProductEventConsumer(IProductEventRepository productEventRepository)
        {
            _productEventRepository = productEventRepository;
        }

        public async Task Consume(ConsumeContext<IProductEvent> context)
        {
            var msg = context.Message;

            var type = Enum.Parse<ProductEventType>(msg.EventType);

            var entity = new ProductEvent(
                msg.ProductId,
                type,
                msg.Name,
                msg.Category,
                msg.UnitCost
            );

           await _productEventRepository.AddAsync( entity );

        }
    }
}
