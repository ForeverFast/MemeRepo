using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.AppData.Reducers.FolderActionsReducers
{
    internal static class CreateFolderActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceCreateFolderAction(AppDataState state, CreateFolderAction _) =>
           state with { };

        [ReducerMethod]
        public static AppDataState ReduceCreateFolderSuccessAction(AppDataState state, CreateFolderSuccessAction action)
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
        public static AppDataState ReduceCreateFolderFailureAction(AppDataState state, CreateFolderFailureAction action) =>
            state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
