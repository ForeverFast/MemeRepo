namespace Web.Core.Models
{
    public record TagViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;



        public bool IsSelected { get; set; } = false;
    }
}
