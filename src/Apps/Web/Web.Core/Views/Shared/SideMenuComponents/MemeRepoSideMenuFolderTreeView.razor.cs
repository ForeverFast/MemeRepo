using Web.Core.Models.Components;
using Web.Core.Store.App;
using Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions;
using Web.Core.Store.App.Actions.DataActions.SetCurrentContentActions;
using Web.Core.Store.App.Actions.FolderActions.CreateFolderActions;
using Web.Core.Store.App.Actions.FolderActions.DeleteFolderActions;
using Web.Core.Store.App.Actions.FolderActions.UpdateFolderActions;

namespace Web.Core.Views.Shared.SideMenuComponents
{
    public partial class MemeRepoSideMenuFolderTreeView : MemeRepoBaseFluxorComponent
    {
        #region Injects

        [Inject] IState<AppState>? _appState { get; init; }
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

        private void OnFolderTreeViewItemClick(FolderTreeViewModel context) => _navigationManager!.NavigateToFolder(context.Id);

        #endregion
    }
}