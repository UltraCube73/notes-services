namespace NotesFEService.Data.DTO
{
    public class NoteCreationInfo
    {
        public required string Text { get; set; }
        public required Guid CategoryId { get; set; }
    }
}