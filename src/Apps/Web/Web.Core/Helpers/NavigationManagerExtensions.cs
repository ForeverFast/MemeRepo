namespace Web.Core.Helpers
{
    internal static class NavigationManagerExtensions
    {
        public static void NavigateToFolder(this NavigationManager navigationManager, Guid folderId)
            => navigationManager.NavigateTo($"folder/{folderId}");
        public static void NavigateToTag(this NavigationManager navigationManager, Guid tagId)
            => navigationManager.NavigateTo($"tag/{tagId}");
    }
}
