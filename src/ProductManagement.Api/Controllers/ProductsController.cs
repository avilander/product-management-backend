using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Abstractions;
using ProductManagement.Application.Products.Dtos;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IUseCase<Unit, IEnumerable<ProductDTO>> _getAllProducts;
        private readonly IUseCase<Guid, ProductDTO?> _getById;
        private readonly IUseCase<CreateProductDTO, ProductDTO> _create;
        private readonly IUseCase<(Guid id, UpdateProductDTO dto), ProductDTO?> _update;
        private readonly IUseCase<Guid, bool> _delete;

        public ProductsController(
            IUseCase<Unit, IEnumerable<ProductDTO>> getAllProducts,
            IUseCase<Guid, ProductDTO?> getById,
            IUseCase<CreateProductDTO, ProductDTO> create,
            IUseCase<(Guid id, UpdateProductDTO dto), ProductDTO?> update,
            IUseCase<Guid, bool> delete)
        {
            _getAllProducts = getAllProducts;
            _getById = getById;
            _create = create;
            _update = update;
            _delete = delete;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var products = await _getAllProducts.ExecuteAsync(Unit.Value, cancellationToken);
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _getById.ExecuteAsync(id, cancellationToken);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] CreateProductDTO DTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _create.ExecuteAsync(DTO, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/products/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> Update(Guid id, [FromBody] UpdateProductDTO DTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _update.ExecuteAsync((id, DTO), cancellationToken);

            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _delete.ExecuteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
