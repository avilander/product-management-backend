using ProductManagement.Application.Products.Dtos;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Dtos
{

    public sealed record ProductDTO(
        Guid Id,
        string Name,
        string Category,
        decimal UnitCost,
        DateTime CreatedAt
    )
    {
        // Conversão implícita: Product → ProductDto
        public static implicit operator ProductDTO(Product product)
            => new ProductDTO(
                product.Id,
                product.Name,
                product.Category,
                product.UnitCost,
                product.CreatedAt
            );
    }
}
