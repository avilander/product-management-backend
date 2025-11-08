using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Abstractions;
using ProductManagement.Application.Products.Dtos;
using ProductManagement.Application.Products.UseCases;
using ProductManagement.Application.Products.UseCases.Impl;


namespace ProductManagement.Application.IoC
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<CreateProductDTO, ProductDTO>, CreateProductUseCase>();
            services.AddScoped<IUseCase<Unit, IEnumerable<ProductDTO>>, GetAllProductsUseCase>();
            services.AddScoped<IUseCase<Guid, ProductDTO?>, GetProductByIdUseCase>();
            services.AddScoped<IUseCase<(Guid id, UpdateProductDTO dto), ProductDTO?>, UpdateProductUseCase>();
            services.AddScoped<IUseCase<Guid, bool>, DeleteProductUseCase>();
            
            return services;
        }
    }
}
