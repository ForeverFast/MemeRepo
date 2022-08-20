
using Web.Core.Store.App.Actions.FolderActions.UpdateFolderActions;

namespace Web.Core.Store.App.Reducers.FolderActionsReducers
{
    internal static class UpdateFolderActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceUpdateFolderAction(AppState state, UpdateFolderAction _)
            => state with { };

        [ReducerMethod]
        public static AppState ReduceUpdateFolderSuccessAction(AppState state, UpdateFolderSuccessAction action)
        {
            var targetFolder = state.FolderList.First(x => x.Id == action.UpdatedFolder.Id);

            targetFolder.Title = action.UpdatedFolder.Title;
            targetFolder.Description = action.UpdatedFolder.Description;
            targetFolder.Path = action.UpdatedFolder.Path;

            return state;
        }

        [ReducerMethod]
        public static AppState ReduceUpdateFolderFailureAction(AppState state, UpdateFolderFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
