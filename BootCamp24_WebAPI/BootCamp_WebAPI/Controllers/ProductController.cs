using BootCamp24_Domain.Interfaces.Repository;
using BootCamp24_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootCamp_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var products = await _repository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(Guid id)
        {
            var product = await _repository.GetById(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProduct(ProductModel product)
        {
            product.IsActive = true;

            await _repository.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductModel product)
        {
            if (id != product.Id) return BadRequest("The ID in the URL does not match the ID in the request body.");

            if (!await _repository.Exists(id)) return NotFound();

            await _repository.Update(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _repository.GetById(id);
            if (product == null) return NotFound();

            await _repository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateProduct(Guid id)
        {
            var product = await _repository.GetById(id);
            if (product == null) return NotFound();

            product.IsActive = false;
            await _repository.Update(product);

            return NoContent();
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateProduct(Guid id)
        {
            var product = await _repository.GetById(id);
            if (product == null) return NotFound();

            product.IsActive = true;
            await _repository.Update(product);

            return NoContent();
        }

        private async Task<bool> ProductExists(Guid id)
        {
            return await _repository.Exists(id);
        }
    }
}
