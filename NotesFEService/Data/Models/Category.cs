namespace NotesFEService.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; } = new Guid();
        public required Guid OwnerId { get; set; }
        public required string Name { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}