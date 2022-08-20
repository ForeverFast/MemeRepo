using Web.Core.Store.App.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.App.Reducers.FolderActionsReducers
{
    internal static class CreateFolderActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceCreateFolderAction(AppState state, CreateFolderAction _)
            => state with { };

        [ReducerMethod]
        public static AppState ReduceCreateFolderSuccessAction(AppState state, CreateFolderSuccessAction action)
        {
            if (!action.NewFolder.ParentFolderId.HasValue)
            {
                state.Folders.Add(action.NewFolder);
                return state;
            }

            var parentFolder = state.Folders.SelectManyRecursive(x => x.Folders).First(x => x.Id == action.NewFolder.ParentFolderId);
            action.NewFolder.ParentFolder = parentFolder;
            parentFolder.Folders.Add(action.NewFolder);
            return state;
        }

        [ReducerMethod]
        public static AppState ReduceCreateFolderFailureAction(AppState state, CreateFolderFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
