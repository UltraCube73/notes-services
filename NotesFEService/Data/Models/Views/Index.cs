using Microsoft.AspNetCore.Mvc.Rendering;

namespace NotesFEService.Data.Models.Views
{
    public class Index
    {
        public string? CurrentCategory { get; set; }
        public string? NewCategoryName { get; set; }
        public string? StatusMessage { get; set; }
        public List<SelectListItem> Options { get; set; } = new List<SelectListItem>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public bool HasOptions { get; set; } = false;
        public required User User { get; set; }
    }
}