namespace Web.Core.Models.Components.Search
{
    internal record FilterModel
    {
        public string SearchTitle { get; init; } = string.Empty;
        public List<Guid> Tags { get; init; } = new();
    }
}
