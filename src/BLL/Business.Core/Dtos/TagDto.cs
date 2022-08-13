namespace Business.Core.Dtos
{
    public record TagDto
    {
        public Guid Id { get; init; }

        public string Title { get; init; } = string.Empty;   
    }
}
