using Web.Core.Store.App.Actions.FolderActions.DeleteFolderActions;

namespace Web.Core.Store.App.Reducers.FolderActionsReducers
{
    internal static class DeleteFolderActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceDeleteFolderAction(AppState state, DeleteFolderAction _)
            => state with
            {
            };

        [ReducerMethod]
        public static AppState ReduceDeleteFolderSuccessAction(AppState state, DeleteFolderSuccessAction action)
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
        public static AppState ReduceDeleteFolderFailureAction(AppState state, DeleteFolderFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
