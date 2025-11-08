using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Repositories
{
    public interface IProductEventRepository
    {

        /// <summary>
        /// Adiciona um novo evento na tabela de produtos eventos.
        /// </summary>
        Task AddAsync(ProductEvent product, CancellationToken cancellationToken = default);
    }
}
