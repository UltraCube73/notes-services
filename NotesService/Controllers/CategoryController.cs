using Microsoft.AspNetCore.Mvc;
using NotesService.Data;
using NotesService.Data.DTO;
using NotesService.Data.Model;
using NotesService.Data.Repositories;

namespace NotesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(NotesDbContext dbContext)
        {
            _categoryRepo = new CategoryRepository(dbContext);
        }

        [HttpGet]
        public Category Get(string id)
        {
            return _categoryRepo.GetById(id);
        }

        [HttpGet("by-user")]
        public List<Category> ByUser(string id)
        {
            return _categoryRepo.GetByUser(id);
        }

        [HttpPost("create")]
        public IActionResult Create(Category category)
        {
            _categoryRepo.Create(category);
            return Ok();
        }

        [HttpPost("delete")]
        public IActionResult Delete(CategoryId categoryId)
        {
            _categoryRepo.Delete(categoryId.id);
            return Ok();
        }
    }
}