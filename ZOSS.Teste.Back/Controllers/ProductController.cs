using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using ZOSS.Teste.Back.Data;
using ZOSS.Teste.Back.DTOS;
using ZOSS.Teste.Back.Models;

namespace ZOSS.Teste.Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context) => _context = context;

        [HttpPost]
        public IActionResult Post(ProductCreateDto dto)
        {
            var categoryExists = _context.Categories.Any(c => c.Id == dto.CategoryId);
            if (!categoryExists)
            {
                return BadRequest($"A categoria com ID {dto.CategoryId} não existe.");
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Value = dto.Value,
                CategoryId = dto.CategoryId
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
        }

        [HttpGet("/products")]
        public IActionResult GetAll()
        {
            var products = _context.Products
               .Include(p => p.Category)
               .Select(p => new ProductRespDto
               {
                   Id = p.Id,
                   Name = p.Name,
                   Value = (decimal)p.Value,
                   Category = p.Category.Name,
                   Description = p.Description
               }).ToList();

            return Ok(products);
        }

        [HttpPut]
        public IActionResult Put(ProductPutDto obj)
        {
            var product = _context.Products.Find(obj.Id);
            if (product == null) return NotFound();

            product.Name = obj.Name;
            product.Description = obj.Description;
            product.Value = obj.Value;
            product.CategoryId = obj.CategoryId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
