using Web.Core.Enums.Components;
using Web.Core.Models.Components;
using Web.Core.Store.App.Actions.FolderActions.DeleteFolderActions;
using Web.Core.Store.App.Actions.FolderActions.UpdateFolderActions;
using Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions;
using Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Components.ItemsContainer
{
    public partial class MemeRepoContainer
    {
        #region Params

        [Parameter] public List<MemeRepoItemViewModel> Items { get; set; } = new();
        [Parameter] public RenderFragment? EmptyContent { get; set; }

        #endregion

        #region Injects

        [Inject] IDispatcher? _dispatcher { get; init; }

        #endregion

        #region Internal methods

        protected void OnMemeRepoItemContextMenuUpdateMemeClick(MemeRepoItemViewModel item)
        {
            switch (item.FolderObjectType)
            {
                case MemeRepoItemType.Folder:
                    _dispatcher!.Dispatch(new UpdateFolderAction(item.Id));
                    break;
                case MemeRepoItemType.Img:
                    _dispatcher!.Dispatch(new UpdateMemeAction(item.Id));
                    break;
            }
        }

        protected void OnMemeRepoItemContextMenuDeleteDeleteClick(MemeRepoItemViewModel item)
        {
            switch (item.FolderObjectType)
            {
                case MemeRepoItemType.Folder:
                    _dispatcher!.Dispatch(new DeleteFolderAction(item.Id));
                    break;
                case MemeRepoItemType.Img:
                    _dispatcher!.Dispatch(new DeleteMemeAction(item.Id));
                    break;
            }
        }

        #endregion
    }
}
