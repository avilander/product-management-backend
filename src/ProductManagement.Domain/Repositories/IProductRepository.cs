using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Retorna todos os produtos cadastrados.
        /// </summary>
        Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Busca um produto pelo ID.
        /// </summary>
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adiciona um novo produto.
        /// </summary>
        Task AddAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Atualiza os dados de um produto.
        /// </summary>
        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove um produto do banco de dados.
        /// </summary>
        Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
    }
}
