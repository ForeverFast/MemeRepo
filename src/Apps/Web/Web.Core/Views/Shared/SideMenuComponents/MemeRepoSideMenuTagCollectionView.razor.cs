﻿using Web.Core.Models;
using Web.Core.Store.AppData;
using Web.Core.Store.AppData.Actions.ChangeStateActions.SetCurrentContentActions;
using Web.Core.Store.AppData.Actions.TagActions.CreateTagActions;
using Web.Core.Store.AppData.Actions.TagActions.DeleteTagActions;
using Web.Core.Store.AppData.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Views.Shared.SideMenuComponents
{
    public partial class MemeRepoSideMenuTagCollectionView : MemeRepoBaseFluxorComponent
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

            SubscribeToAction<CreateTagSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<UpdateTagSuccessAction>(BaseUpdateUIAction);
            SubscribeToAction<DeleteTagSuccessAction>(BaseUpdateUIAction);

            base.OnInitialized();
        }

        #endregion

        #region Internal Events

        protected void OnTagClick(TagViewModel context)
            => _navigationManager!.NavigateToTag(context.Id);

        protected void OnCollectionViewContextMenuCreateTagClick()
            => _dispatcher!.Dispatch(new CreateTagAction());

        protected void OnTagContextMenuUpdateTagClick(Guid tagId)
            => _dispatcher!.Dispatch(new UpdateTagAction(tagId));
        protected void OnTagContextMenuDeleteTagClick(Guid tagId)
            => _dispatcher!.Dispatch(new DeleteTagAction(tagId));

        #endregion
    }
}