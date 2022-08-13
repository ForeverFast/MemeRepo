using Domain.Core.Extensions;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components.Routing;
using Web.Core.Components.DialogComponents;
using Web.Core.Models.Components;
using Web.Core.Store.AppData;
using Web.Core.Store.AppData.Actions.ChangeStateActionsEffects;
using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Views.Shared.SideMenuComponents
{
    public partial class MemeRepoSideMenuFolderTreeView : FluxorComponent
    {
        #region Injects

        [Inject] IState<AppDataState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }
        [Inject] IActionSubscriber? _actionSubscriber { get; init; }

        [Inject] NavigationManager? _navigationManager { get; init; }
        [Inject] IDialogService? _dialogService { get; init; }

        [Inject] IMapper? _mapper { get; init; }

        #endregion

        #region UI Fields

        protected bool _isComponentInitialized = false;

        protected List<FolderTreeViewModel> SelectedFolderPath = new();

        #endregion

        #region State methods

        protected override async Task OnInitializedAsync()
        {
            SubscribeToAction<SetCurrentFolderAction>(OnSetCurrentFolderAction);

            TryInitializeElement();
            _isComponentInitialized = true;

            await base.OnInitializedAsync();
        }

        protected override void Dispose(bool disposing)
        {
            _actionSubscriber!.UnsubscribeFromAllActions(this);
            base.Dispose(disposing);
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

        protected void OnFolderTreeViewItemClick(FolderTreeViewModel context) => _navigationManager!.NavigateToFolder(context.Id);
        protected Task OnTreeViewContextMenuCreateFolderClick() => CreateFolderAsync(null);
        protected Task OnTreeViewItemContextMenuCreateFolderClick(Guid? parentFolderId) => CreateFolderAsync(parentFolderId);

        #endregion

        #region Private methods

        protected async Task CreateFolderAsync(Guid? parentFolderId)
        {
            var dialog = _dialogService!.Show<FolderDialog>("Создать папку", FolderDialog.DialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var folder = (FolderDialogViewModel)result.Data;
                folder.ParentFolderId = parentFolderId;

                var action = _mapper!.Map<CreateFolderAction>(folder);
                _dispatcher!.Dispatch(action);
            }
        }

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