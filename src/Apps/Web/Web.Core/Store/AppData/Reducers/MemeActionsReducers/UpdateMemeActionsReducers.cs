using Web.Core.Enums.Components;
using Web.Core.Models.Components;
using Web.Core.Store.AppData.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Store.AppData.Reducers.MemeActionsReducers
{
    internal static class UpdateMemeActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderAction(AppDataState state, UpdateMemeAction _)
            => state with { };

        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderSuccessAction(AppDataState state, UpdateMemeSuccessAction action)
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
                state.Items.Add(memeRepoItem);

            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderFailureAction(AppDataState state, UpdateMemeFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
