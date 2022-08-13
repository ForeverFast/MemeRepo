namespace Web.Core.Models
{
    public record TagViewModel
    {
        public Guid Id { get; init; }

        public string Title { get; init; } = string.Empty;
    }
}
