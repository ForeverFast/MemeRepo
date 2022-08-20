using Web.Core.Enums.Components;
using Web.Core.Models.Components;
using Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Store.App.Reducers.MemeActionsReducers
{
    internal static class UpdateMemeActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceUpdateFolderAction(AppState state, UpdateMemeAction _)
            => state with { };

        [ReducerMethod]
        public static AppState ReduceUpdateFolderSuccessAction(AppState state, UpdateMemeSuccessAction action)
        {
            var memeRepoItem = new MemeRepoItemViewModel
            {
                Id = action.UpdatedMeme.Id,
                ParentFolderId = action.UpdatedMeme.ParentFolderId,
                FolderObjectType = MemeRepoItemType.Img,
                Path = state.GetFileRelativePath(action.UpdatedMeme.ParentFolderId, action.UpdatedMeme.Path),
                Title = action.UpdatedMeme.Title,
            };

            bool contentTypeCheck = (state.CurrentContentType & (ContentType.FolderPage | ContentType.TagPage | ContentType.SearchPage)) > 0;
            bool parentFolderCheck = state.CurrentContentId == action.UpdatedMeme.ParentFolderId;
            bool filterCheck = state.Validate(memeRepoItem, action.UpdatedMeme.Tags);

            if (contentTypeCheck && (parentFolderCheck || filterCheck))
            {
                var targetItem = state.Items.First(x => x.Id == action.UpdatedMeme.Id);

                targetItem.Title = action.UpdatedMeme.Title;
            }
                

            return state;
        }

        [ReducerMethod]
        public static AppState ReduceUpdateFolderFailureAction(AppState state, UpdateMemeFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
