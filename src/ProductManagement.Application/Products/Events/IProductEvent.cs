using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Events
{
    public interface IProductEvent
    {
        Guid ProductId { get; }
        string EventType { get; } // "Created", "Updated", "Deleted"
        string Name { get; }
        string Category { get; }
        decimal UnitCost { get; }
        DateTime OccurredAt { get; }
    }
}
