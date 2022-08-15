using Fluxor.Blazor.Web.Components;
using Web.Core.Models.Components;
using Web.Core.Store.AppData;
using Web.Core.Store.AppData.Actions.ChangeStateActionsEffects;
using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;
using Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions;
using Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions;
using Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Views.Shared.SideMenuComponents
{
    public partial class MemeRepoSideMenuFolderTreeView : FluxorComponent
    {
        #region Injects

        [Inject] IState<AppDataState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }
        [Inject] IActionSubscriber? _actionSubscriber { get; init; }
        [Inject] NavigationManager? _navigationManager { get; init; }

        #endregion

        #region UI Fields

        protected bool _isComponentInitialized = false;

        protected List<FolderTreeViewModel> SelectedFolderPath = new();

        #endregion

        #region State methods

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnInitializedAsync()
        {
            SubscribeToAction<SetCurrentFolderAction>(OnSetCurrentFolderAction);
            SubscribeToAction<CreateFolderSuccessAction>((action) => InvokeAsync(() => this.StateHasChanged()));
            SubscribeToAction<UpdateFolderSuccessAction>((action) => InvokeAsync(() => this.StateHasChanged()));
            SubscribeToAction<DeleteFolderSuccessAction>((action) => InvokeAsync(() => this.StateHasChanged()));

            TryInitializeElement();
            _isComponentInitialized = true;

            await base.OnInitializedAsync();
        }

        #endregion

        #region External Events

        private void OnSetCurrentFolderAction(SetCurrentFolderAction action)
            => _ = InvokeAsync(() =>
            {
                if (TryInitializeElement())
                    this.StateHasChanged();
            });

        #endregion

        #region Internal Events

        protected void OnFolderTreeViewItemClick(FolderTreeViewModel context)
            => _navigationManager!.NavigateToFolder(context.Id);
        protected void OnTreeViewContextMenuCreateFolderClick() 
            => _dispatcher!.Dispatch(new CreateFolderAction(null));
        protected void OnTreeViewItemContextMenuCreateFolderClick(Guid? parentFolderId)
            => _dispatcher!.Dispatch(new CreateFolderAction(parentFolderId));
        protected void OnTreeViewItemContextMenuCreateMemeClick(Guid parentFolderId)
            => _dispatcher!.Dispatch(new CreateMemeAction(parentFolderId));
        protected void OnTreeViewItemContextMenuUpdateFolderClick(Guid folderId)
            => _dispatcher!.Dispatch(new UpdateFolderAction(folderId));
        protected void OnTreeViewItemContextMenuDeleteFolderClick(Guid folderId)
            => _dispatcher!.Dispatch(new DeleteFolderAction(folderId));

        #endregion

        #region Private methods

        protected bool TryInitializeElement()
        {
            if (_appState!.Value.CurrentFolder != null)
            {
                var oldSelectedFolderPath = SelectedFolderPath;
                SelectedFolderPath = _appState!.Value.CurrentFolder!.SelectRecursive(x => x.ParentFolder).ReverseToList();

                oldSelectedFolderPath?.ForEach(x =>
                {
                    if (x.IsExpanded && !x.HasChild)
                        x.IsExpanded = false;
                    x.PathFlag = false;
                });

                SelectedFolderPath.ForEach(x =>
                {
                    if (!_isComponentInitialized)
                        x.IsExpanded = true;
                    x.PathFlag = true;
                });

                return true;
            }

            return false;
        }

        #endregion
    }
}