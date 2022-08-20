using Web.Core.Store.App.Actions.TagActions.UpdateTagActions;

namespace Web.Core.Store.App.Reducers.TagActionsReducers
{
    internal static class UpdateTagActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceUpdateFolderAction(AppState state, UpdateTagAction _)
            => state with { };

        [ReducerMethod]
        public static AppState ReduceUpdateFolderSuccessAction(AppState state, UpdateTagSuccessAction action)
        {
            var targetFolder = state.Tags.First(x => x.Id == action.UpdatedTag.Id);

            targetFolder.Title = action.UpdatedTag.Title;

            return state;
        }

        [ReducerMethod]
        public static AppState ReduceUpdateFolderFailureAction(AppState state, UpdateTagFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
