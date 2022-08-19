using Fluxor.Blazor.Web.Components;
using Web.Core.Base.Components;
using Web.Core.Models.Components;
using Web.Core.Store.AppData;
using Web.Core.Store.AppData.Actions.ChangeStateActions.SetCurrentContentActions;
using Web.Core.Store.AppData.Actions.DataActions.AddFilesFromDiskActions;
using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;
using Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions;
using Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions;
using Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Views.Shared.SideMenuComponents
{
    public partial class MemeRepoSideMenuFolderTreeView : MemeRepoBaseFluxorComponent
    {
        #region Injects

        [Inject] IState<AppDataState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }
        [Inject] NavigationManager? _navigationManager { get; init; }

        #endregion

        #region UI Fields

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            SubscribeToAction<SetCurrentContentAction>(BaseUpdateUIAction);
            SubscribeToAction<CreateFolderSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<UpdateFolderSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<DeleteFolderSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<AddFilesFromDiskSuccessAction>(BaseUpdateUIAction);

            base.OnInitialized();
        }

        #endregion

        #region Internal Events

        protected void OnFolderTreeViewItemClick(FolderTreeViewModel context)
            => _navigationManager!.NavigateToFolder(context.Id);

        protected void OnTreeViewContextMenuCreateFolderClick()
            => _dispatcher!.Dispatch(new CreateFolderAction(null));
        protected void OnTreeViewContextMenuAddFilesFromDiskClick()
           => _dispatcher!.Dispatch(new AddFilesFromDiskAction(null));

        protected void OnTreeViewItemContextMenuCreateFolderClick(Guid? parentFolderId)
            => _dispatcher!.Dispatch(new CreateFolderAction(parentFolderId));
        protected void OnTreeViewItemContextMenuCreateMemeClick(Guid parentFolderId)
            => _dispatcher!.Dispatch(new CreateMemeAction(parentFolderId));
        protected void OnTreeViewItemContextMenuAddFilesFromDiskClick(Guid parentFolderId)
           => _dispatcher!.Dispatch(new AddFilesFromDiskAction(parentFolderId));
        protected void OnTreeViewItemContextMenuUpdateFolderClick(Guid folderId)
            => _dispatcher!.Dispatch(new UpdateFolderAction(folderId));
        protected void OnTreeViewItemContextMenuDeleteFolderClick(Guid folderId)
            => _dispatcher!.Dispatch(new DeleteFolderAction(folderId));

        #endregion
    }
}