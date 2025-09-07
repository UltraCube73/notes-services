namespace NotesFEService.Data.Models
{
    public class Category
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required List<Note> Notes { get; set; }
    }
}