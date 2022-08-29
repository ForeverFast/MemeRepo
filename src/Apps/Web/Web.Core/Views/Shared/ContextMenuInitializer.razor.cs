using Domain.Core.Interfaces;
using Web.Core.Models;
using Web.Core.Store.App;
using Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions;
using Web.Core.Store.App.Actions.FolderActions.CreateFolderActions;
using Web.Core.Store.App.Actions.FolderActions.DeleteFolderActions;
using Web.Core.Store.App.Actions.FolderActions.UpdateFolderActions;
using Web.Core.Store.App.Actions.MemeActions.CreateMemeActions;
using Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions;
using Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions;
using Web.Core.Store.App.Actions.TagActions.CreateTagActions;
using Web.Core.Store.App.Actions.TagActions.DeleteTagActions;
using Web.Core.Store.App.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Views.Shared
{
    public partial class ContextMenuInitializer
    {
        #region Injects

        [Inject] IState<AppState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }
        [Inject] NavigationManager? _navigationManager { get; init; }

        [Inject] IFileStorageProvider? _fileStorageProvider { get; init; }

        #endregion

        #region Folder

        private void OnFolderContextMenuOpenClick(Guid? folderId) => _navigationManager!.NavigateToFolder(folderId);
        private void OnFolderContextMenuCreateFolderClick(Guid? parentFolderId) => _dispatcher!.Dispatch(new CreateFolderAction(parentFolderId));
        private void OnFolderContextMenuCreateMemeClick(Guid? parentFolderId) => _dispatcher!.Dispatch(new CreateMemeAction(parentFolderId));
        private void OnFolderContextMenuUpdateClick(Guid folderId) => _dispatcher!.Dispatch(new UpdateFolderAction(folderId));
        private void OnFolderContextMenuDeleteClick(Guid folderId) => _dispatcher!.Dispatch(new DeleteFolderAction(folderId));

        private void OnFolderContextMenuOpenInExplorerClick(Guid? folderId) => _fileStorageProvider!.ShowFolder(_appState!.Value.GetFolderRelativePath(folderId));
        private void OnFolderContextMenuAddFilesFromDiskClick(Guid? parentFolderId) => _dispatcher!.Dispatch(new AddFilesFromDiskAction(parentFolderId));

        #endregion

        #region Meme

        private void OnMemeContextMenuOpenClick(Guid memeId) => throw new NotImplementedException();
        private void OnMemeContextMenuUpdateClick(Guid memeId) => _dispatcher!.Dispatch(new UpdateMemeAction(memeId));
        private void OnMemeContextMenuDeleteClick(Guid memeId) => _dispatcher!.Dispatch(new DeleteMemeAction(memeId));

        private void OnMemeContextMenuOpenInExplorerClick(Guid memeId) => _fileStorageProvider!.ShowFolder(_appState!.Value.GetFileFolderRelativePath(memeId));

        #endregion

        #region Tag

        protected void OnTagContextMenuNavigationClick(Guid tagId) => _navigationManager!.NavigateToTag(tagId);
        protected void OnTagContextMenuCreateTagClick() => _dispatcher!.Dispatch(new CreateTagAction());
        protected void OnTagContextMenuUpdateTagClick(Guid tagId) => _dispatcher!.Dispatch(new UpdateTagAction(tagId));
        protected void OnTagContextMenuDeleteTagClick(Guid tagId) => _dispatcher!.Dispatch(new DeleteTagAction(tagId));

        #endregion
    }
}
