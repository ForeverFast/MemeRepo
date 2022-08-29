using Web.Core.Enums.Components.StateContainer;
using Web.Core.Extensions;
using Web.Core.Models.Components;
using Web.Core.Models.Components.Search;
using Web.Core.Store.App.Actions.DataActions.AddFilesFromDiskActions;
using Web.Core.Store.App.Actions.DataActions.SetCurrentContentActions;
using Web.Core.Store.App.Actions.MemeActions.CreateMemeActions;
using Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions;
using Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Views.Pages
{
    [Route("search-page/{Salt:guid}")]
    public partial class SearchPage : MemeRepoBaseFluxorComponent
    {
        #region Query params

        public string SearchTitle = string.Empty;

        #endregion

        #region Params

        [Parameter] public Guid Salt { get; set; }

        #endregion

        #region Injects

        [Inject] IDispatcher? _dispatcher { get; init; }
        [Inject] NavigationManager? _navigationManager { get; init; }

        #endregion

        #region UI Fields

        private ComponentState state = ComponentState.Loading;

        private List<MemeRepoItemViewModel> searchObjects = new();

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
            _navigationManager!.TryGetQueryString(nameof(SearchTitle), out SearchTitle);

            searchObjects.Clear();

            _dispatcher!.Dispatch(new SetCurrentContentAction(ContentType.SearchPage)
            {
                Filter = new FilterModel
                {
                    SearchTitle = SearchTitle,
                    // TODO
                    // Tags = 
                }
            });

            base.OnParametersSet();
        }

        #endregion

        #region External events

        private void OnSetCurrentContentSuccessAction(SetCurrentContentSuccessAction action)
        {
            searchObjects = action.Items;
            state = ComponentState.Content;
        }

        private void OnSetCurrentContentFailureAction(SetCurrentContentFailureAction action)
        {
            state = ComponentState.Error;
        }

        #endregion
    }
}
