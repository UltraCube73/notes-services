namespace NotesFEService.Data.Models.Views
{
    public class Edit
    {
        public string NoteText { get; set; } = "";

        public string? NoteId { get; set; } = null;

        public bool IsExisting { get; set; }

        public string StatusMessage { get; set; } = "";

        public string categoryId { get; set; } = "";
    }
}