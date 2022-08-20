using Web.Core.Store.App.Actions.DataActions.LoadAppDataActions;

namespace Web.Core.Store.App.Reducers.DataActionsReducers
{
    internal static class LoadAppDataActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceLoadTodosAction(AppState state, LoadAppDataAction _)
            => state with { IsLoading = true };

        [ReducerMethod]
        public static AppState ReduceLoadTodosSuccessAction(AppState state, LoadAppDataSuccessAction action)
            => state with
            {
                Folders = action.Folders,
                Tags = action.Tags,
                IsLoading = false,
                CurrentErrorMessage = string.Empty,
            };

        [ReducerMethod]
        public static AppState ReduceLoadTodosFailureAction(AppState state, LoadAppDataFailureAction action)
            => state with
            {
                IsLoading = false,
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
