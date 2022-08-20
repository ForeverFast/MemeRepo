namespace Web.Core.Models.Components.Dialogs
{
    public class ItemTagViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public bool IsSelected { get; set; }
    }
}
