using ProductManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Entities
{
    public class ProductEvent
    {
        public Guid Id { get; private set; }           // Id do evento
        public Guid ProductId { get; private set; }    // Id do produto
        public ProductEventType EventType { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public decimal UnitCost { get; private set; }
        public DateTime OccurredAt { get; private set; }

        protected ProductEvent() { }

        public ProductEvent(Guid productId, ProductEventType type, string name, string category, decimal unitCost)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            EventType = type;
            Name = name;
            Category = category;
            UnitCost = unitCost;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
