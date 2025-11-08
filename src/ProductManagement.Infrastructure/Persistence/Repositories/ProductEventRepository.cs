using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class ProductEventRepository : IProductEventRepository
    {

        private readonly ProductManagementDBContext _dbContext;

        public ProductEventRepository(ProductManagementDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
        }

        public async Task AddAsync(ProductEvent product, CancellationToken cancellationToken = default)
        {
            await _dbContext.ProductEvents.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
