namespace NotesService.Data.Model
{
    public class Note
    {
        public Guid Id { get; set; } = new Guid();
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }
        public required string Text { get; set; }
    }
}