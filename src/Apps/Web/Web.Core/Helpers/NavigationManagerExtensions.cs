namespace Web.Core.Helpers
{
    internal static class NavigationManagerExtensions
    {
        public static void NavigateToFolder(this NavigationManager navigationManager, Guid FolderId)
            => navigationManager.NavigateTo($"folder/{FolderId}");
    }
}
