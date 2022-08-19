using Web.Core.Store.AppData.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Store.AppData.Reducers.TagActionsReducers
{
    internal static class UpdateTagActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderAction(AppDataState state, UpdateTagAction _)
            => state with { };

        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderSuccessAction(AppDataState state, UpdateTagSuccessAction action)
        {
            var targetFolder = state.FolderList.First(x => x.Id == action.UpdatedTag.Id);

            targetFolder.Title = action.UpdatedTag.Title;

            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceUpdateFolderFailureAction(AppDataState state, UpdateTagFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
