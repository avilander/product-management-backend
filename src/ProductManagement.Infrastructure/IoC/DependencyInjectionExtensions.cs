
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Domain.Repositories;
using ProductManagement.Infrastructure.Messaging;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Persistence.Repositories;

namespace ProductManagement.Infrastructure.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // DbContext
            services.AddDbContext<ProductManagementDBContext>(options =>options.UseNpgsql(connectionString));

            // Repositórios
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductEventRepository, ProductEventRepository>();

            return services;
        }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                // registra o consumer
                x.AddConsumer<ProductEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:HostName"] ?? "rabbitmq", h =>
                    {
                        h.Username(configuration["RabbitMq:UserName"] ?? "admin");
                        h.Password(configuration["RabbitMq:Password"] ?? "admin");
                    });
                });
            });
            return services;
        }

        public static IServiceCollection AddRabbitMqWorker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                // registra o consumer
                x.AddConsumer<ProductEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:HostName"] ?? "rabbitmq", h =>
                    {
                        h.Username(configuration["RabbitMq:UserName"] ?? "admin");
                        h.Password(configuration["RabbitMq:Password"] ?? "admin");
                    });

                    // endpoint / fila que vai consumir os eventos
                    cfg.ReceiveEndpoint("product_events_queue", e =>
                    {
                        e.ConfigureConsumer<ProductEventConsumer>(context);
                    });
                });
            });
            return services;
        }
    }
}
