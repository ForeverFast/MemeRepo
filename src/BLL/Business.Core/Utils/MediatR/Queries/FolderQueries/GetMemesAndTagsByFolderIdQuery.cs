namespace Business.Core.Utils.MediatR.Queries.FolderQueries
{
    public record GetMemesAndTagsByFolderIdQuery : IRequest<(List<MemeDto> Memes, List<TagDto> Tags)>
    {
        public Guid Id { get; init; }
    }
}
