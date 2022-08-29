using Web.Core.Models;
using Web.Core.Store.App;
using Web.Core.Store.App.Actions.DataActions.SetCurrentContentActions;
using Web.Core.Store.App.Actions.TagActions.CreateTagActions;
using Web.Core.Store.App.Actions.TagActions.DeleteTagActions;
using Web.Core.Store.App.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Views.Shared.SideMenuComponents
{
    public partial class MemeRepoSideMenuTagCollectionView : MemeRepoBaseFluxorComponent
    {
        #region Injects

        [Inject] IState<AppState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }
        [Inject] NavigationManager? _navigationManager { get; init; }

        #endregion

        #region UI Fields

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            SubscribeToAction<SetCurrentContentAction>(BaseUpdateUIAction);

            SubscribeToAction<CreateTagSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<UpdateTagSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<DeleteTagSuccessAction>(BaseUpdateUIAction);

            base.OnInitialized();
        }

        #endregion

        #region Internal Events

        private void OnTagClick(TagViewModel context) => _navigationManager!.NavigateToTag(context.Id);

        #endregion
    }
}
