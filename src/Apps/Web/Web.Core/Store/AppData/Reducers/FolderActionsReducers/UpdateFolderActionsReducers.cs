
using Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions;

namespace Web.Core.Store.AppData.Reducers.FolderActionsReducers
{
    internal static class UpdateFolderActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderAction(AppDataState state, UpdateFolderAction _)
            => state with { };

        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderSuccessAction(AppDataState state, UpdateFolderSuccessAction action)
        {
            var targetFolder = state.FolderList.First(x => x.Id == action.UpdatedFolder.Id);

            targetFolder.Title = action.UpdatedFolder.Title;
            targetFolder.Description = action.UpdatedFolder.Description;
            targetFolder.Path = action.UpdatedFolder.Path;

            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderFailureAction(AppDataState state, UpdateFolderFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
