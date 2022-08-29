﻿using Web.Core.Enums.Components.StateContainer;
using Web.Core.Models.Components;
using Web.Core.Store.App;
using Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions;
using Web.Core.Store.App.Actions.DataActions.SetCurrentContentActions;
using Web.Core.Store.App.Actions.MemeActions.CreateMemeActions;
using Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions;
using Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Views.Pages
{
    [Route("folder/{Id:guid}")]
    public partial class FolderPage : MemeRepoBaseFluxorComponent
    {
        #region Params

        [Parameter] public Guid Id { get; set; }

        #endregion

        #region Injects

        [Inject] IState<AppState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }
        [Inject] NavigationManager? _navigationManager { get; init; }

        #endregion

        #region UI Fields

        private Guid? currentFolderId;
        private ComponentState state = ComponentState.Loading;
        
        private List<MemeRepoItemViewModel> folderObjects = new();
        private List<BreadcrumbItem> folderBreadcrumbItems => _appState!.Value.FolderPath.SelectToList(x => new BreadcrumbItem(x.Title, $"{x.Id}"));

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            SubscribeToAction<CreateMemeSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<UpdateMemeSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<DeleteMemeSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<AddFilesFromDiskSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<SetCurrentContentSuccessAction>(OnSetCurrentContentSuccessAction);
            SubscribeToAction<SetCurrentContentFailureAction>(OnSetCurrentContentFailureAction);

            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            if (currentFolderId != Id)
            {
                state = ComponentState.Loading;
                currentFolderId = Id;
                this.StateHasChanged();
                folderObjects.Clear();

                _dispatcher!.Dispatch(new SetCurrentContentAction(ContentType.FolderPage)
                {
                    CurrentContentId = currentFolderId,
                });
            }

            base.OnParametersSet();
        }

        #endregion

        #region External events

        private void OnSetCurrentContentSuccessAction(SetCurrentContentSuccessAction action)
        {
            folderObjects = action.Items;
            state = ComponentState.Content;
        }
        private void OnSetCurrentContentFailureAction(SetCurrentContentFailureAction action)
        {

            state = ComponentState.Error;
        }

        #endregion

        #region Internal Events

        private void OnFolderBreadcrumbItemClick(string folderId) => _navigationManager!.NavigateToFolder(Guid.Parse(folderId));

        #endregion
    }
}
