using NotesFEService.Data.DTO;

namespace NotesFEService.Data.Models
{
    public class Note
    {
        public Guid Id { get; set; } = new Guid();
        public required Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public required string Text { get; set; }
    }
}