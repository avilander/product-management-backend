using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Application.Products.Dtos
{
    public sealed class UpdateProductDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal UnitCost { get; set; }
    }

}