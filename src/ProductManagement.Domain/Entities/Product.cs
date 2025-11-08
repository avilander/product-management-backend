using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public decimal UnitCost { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Construtor protegido (para ORM)
        protected Product() { }

        public Product(string name, string category, decimal unitCost)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetCategory(category);
            SetUnitCost(unitCost);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));

            Name = name.Trim();
        }

        public void SetCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category is required.", nameof(category));

            Category = category.Trim();
        }

        public void SetUnitCost(decimal cost)
        {
            if (cost <= 0)
                throw new ArgumentException("Unit cost must be greater than zero.", nameof(cost));

            UnitCost = cost;
        }
    }
}
