using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Dtos
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 200 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A categoria é obrigatória")]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O custo unitário deve ser maior que zero")]
        public decimal UnitCost { get; set; }
    }
}
