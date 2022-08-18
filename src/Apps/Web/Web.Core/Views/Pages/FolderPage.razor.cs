using Fluxor.Blazor.Web.Components;
using Web.Core.Enums.Components.StateContainer;
using Web.Core.Models.Components;
using Web.Core.Store.AppData;
using Web.Core.Store.AppData.Actions.ChangeStateActions.SetCurrentContentActions;
using Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions;
using Web.Core.Store.AppData.Actions.MemeActions.DeleteMemeActions;
using Web.Core.Store.AppData.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Views.Pages
{
    [Route("folder/{Id:guid}")]
    public partial class FolderPage : FluxorComponent
    {
        #region Params

        [Parameter] public Guid Id { get; set; }

        #endregion

        #region Injects

        [Inject] IState<AppDataState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }
        [Inject] NavigationManager? _navigationManager { get; init; }

        #endregion

        #region UI Fields

        protected Guid? currentFolderId;
        protected ComponentState state = ComponentState.Loading;

        protected List<MemeRepoItemViewModel> FolderObjects = new();
        protected List<BreadcrumbItem> FolderBreadcrumbItems
            => _appState!.Value.FolderPath.SelectToList(x => new BreadcrumbItem(x.Title, $"{x.Id}"));

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            SubscribeToAction<CreateMemeSuccessAction>((action) => InvokeAsync(() => this.StateHasChanged()));
            SubscribeToAction<UpdateMemeSuccessAction>((action) => InvokeAsync(() => this.StateHasChanged()));
            SubscribeToAction<DeleteMemeSuccessAction>((action) => InvokeAsync(() => this.StateHasChanged()));
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
                FolderObjects.Clear();

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
            FolderObjects = action.Items;
            state = ComponentState.Content;
        }
        private void OnSetCurrentContentFailureAction(SetCurrentContentFailureAction action)
        {

            state = ComponentState.Error;
        }

        #endregion

        #region Internal Events

        protected void OnFolderBreadcrumbItemClick(string folderId) => _navigationManager!.NavigateToFolder(Guid.Parse(folderId));

        #endregion
    }
}
