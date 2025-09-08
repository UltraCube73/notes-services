using Microsoft.AspNetCore.Mvc;
using NotesService.Data;
using NotesService.Data.DTO;
using NotesService.Data.Model;
using NotesService.Data.Repositories;

namespace NotesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepo;

        public NoteController(NotesDbContext dbContext)
        {
            _noteRepo = new NoteRepository(dbContext);
        }

        [HttpGet]
        public Note Get(string id)
        {
            return _noteRepo.GetById(id);
        }

        [HttpGet("by-category")]
        public List<Note> GetByCategory(string id)
        {
            return _noteRepo.GetByCategory(id);
        }

        [HttpPost("create")]
        public IActionResult Create(NoteCreationInfo note)
        {
            _noteRepo.Create(new Note() { Text = note.Text, CategoryId = note.CategoryId });
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update(Note note)
        {
            _noteRepo.Update(note);
            return Ok();
        }

        [HttpPost("delete")]
        public IActionResult Delete(NoteId noteId)
        {
            _noteRepo.Delete(noteId.Id);
            return Ok();
        }
    }
}