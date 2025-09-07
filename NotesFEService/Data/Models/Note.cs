namespace NotesFEService.Data.Models
{
    public class Note
    {
        public required Guid Id { get; set; }
        public required Category Category { get; set; }
        public required string Text { get; set; }
    }
}