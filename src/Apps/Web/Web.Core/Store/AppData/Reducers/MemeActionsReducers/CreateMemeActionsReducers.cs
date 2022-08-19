using Web.Core.Enums.Components;
using Web.Core.Models.Components;
using Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Store.AppData.Reducers.MemeActionsReducers
{
    internal static class CreateMemeActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceCreateFolderAction(AppDataState state, CreateMemeAction _)
            => state with { };

        [ReducerMethod]
        public static AppDataState ReduceCreateFolderSuccessAction(AppDataState state, CreateMemeSuccessAction action)
        {
            var memeRepoItem = new MemeRepoItemViewModel
            {
                Id = action.NewMeme.Id,
                ParentFolderId = action.NewMeme.ParentFolderId,
                FolderObjectType = MemeRepoItemType.Img,
                Path = state.GetFileRelativePath(action.NewMeme.ParentFolderId, action.NewMeme.Path),
                Title = action.NewMeme.Title,
            };

            bool contentTypeCheck = (state.CurrentContentType & (ContentType.FolderPage | ContentType.TagPage | ContentType.SearchPage)) > 0;
            bool parentFolderCheck = state.CurrentContentId == action.NewMeme.ParentFolderId;
            bool filterCheck = state.Validate(memeRepoItem, action.NewMeme.Tags);

            if (contentTypeCheck && (parentFolderCheck || filterCheck))
                state.Items.Add(memeRepoItem);

            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceCreateFolderFailureAction(AppDataState state, CreateMemeFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
