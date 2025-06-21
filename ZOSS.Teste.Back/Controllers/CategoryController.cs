using Microsoft.AspNetCore.Mvc;
using ZOSS.Teste.Back.Data;
using ZOSS.Teste.Back.DTOS;
using ZOSS.Teste.Back.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZOSS.Teste.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context) => _context = context;

        [HttpPost]
        public IActionResult Post(CategoryCreateDto dto)
        {
            var category = new Category { Name = dto.Name };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var result = new CategoryRespDto { Id = category.Id, Name = category.Name };
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories
                .Select(c => new CategoryRespDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            return Ok(categories);
        }
    }
}
