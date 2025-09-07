namespace NotesFEService.Data.Models.Views
{
    public class Edit
    {
        public string NoteText { get; set; } = "";

        public string? NoteID { get; set; } = null;

        public bool IsExisting { get; set; }

        public string StatusMessage { get; set; } = "";
    }
}