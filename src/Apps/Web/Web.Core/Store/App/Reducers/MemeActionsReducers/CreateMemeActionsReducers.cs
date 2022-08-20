using Web.Core.Enums.Components;
using Web.Core.Models.Components;
using Web.Core.Store.App.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Store.App.Reducers.MemeActionsReducers
{
    internal static class CreateMemeActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceCreateFolderAction(AppState state, CreateMemeAction _)
            => state with { };

        [ReducerMethod]
        public static AppState ReduceCreateFolderSuccessAction(AppState state, CreateMemeSuccessAction action)
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
        public static AppState ReduceCreateFolderFailureAction(AppState state, CreateMemeFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
