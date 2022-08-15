using Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions;

namespace Web.Core.Store.AppData.Reducers.FolderActionsReducers
{
    internal static class DeleteFolderActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderAction(AppDataState state, DeleteFolderAction _)
            => state with 
            {
            };

        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderSuccessAction(AppDataState state, DeleteFolderSuccessAction action)
        {
            var targetFolder = state.FolderList.FirstOrDefault(x => x.Id == action.DeletedFolderId);

            if (targetFolder != null)
            {
                if (targetFolder.ParentFolder != null)
                {
                    targetFolder.ParentFolder.Folders.Remove(targetFolder);
                }
                else
                {
                    state.Folders.Remove(targetFolder);
                }
            }
            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderFailureAction(AppDataState state, DeleteFolderFailureAction action) 
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
