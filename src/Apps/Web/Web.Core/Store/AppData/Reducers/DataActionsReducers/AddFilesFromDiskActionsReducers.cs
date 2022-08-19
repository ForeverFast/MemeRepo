using Web.Core.Enums.Components;
using Web.Core.Models.Components;
using Web.Core.Store.AppData.Actions.DataActions.AddFilesFromDiskActions;

namespace Web.Core.Store.AppData.Reducers.DataActionsReducers
{
    internal static class AddFilesFromDiskActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceAddFilesFromDiskAction(AppDataState state, AddFilesFromDiskAction _)
            => state with { ShowFileDropBlock = false };

        [ReducerMethod]
        public static AppDataState ReduceAddFilesFromDiskSuccessAction(AppDataState state, AddFilesFromDiskSuccessAction action)
        {
            var parentFolder = state.Folders.SelectManyRecursive(x => x.Folders).First(x => x.Id == action.ParentFolderId);

            foreach (var newFolder in action.NewFolders)
            {
                if (!newFolder.ParentFolderId.HasValue)
                {
                    state.Folders.Add(newFolder);
                }
                else
                {
                    newFolder.ParentFolder = parentFolder;
                    parentFolder.Folders.Add(newFolder);
                }
            }

            if (!action.ParentFolderId.HasValue)
            {
                // TODO
                //foreach (var newMeme in action.NewMemes)
                //state.Memes.Add(newMeme);
            }
            else if (state.CurrentContentType == ContentType.FolderPage && state.CurrentContentId == action.ParentFolderId)
            {
                foreach (var newMeme in action.NewMemes)
                {
                    state.Items.Add(new MemeRepoItemViewModel
                    {
                        Id = newMeme.Id,
                        ParentFolderId = newMeme.ParentFolderId,
                        FolderObjectType = MemeRepoItemType.Img,
                        Path = state.GetFileRelativePath(newMeme.ParentFolderId, newMeme.Path),
                        Title = newMeme.Title,
                    });
                }
            }

            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceCreateFolderFailureAction(AppDataState state, AddFilesFromDiskFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
