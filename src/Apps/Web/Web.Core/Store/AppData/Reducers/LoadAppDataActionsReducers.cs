using Web.Core.Store.AppData.Actions.LoadAppDataActions;

namespace Web.Core.Store.AppData.Reducers
{
    internal static class LoadAppDataActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceLoadTodosAction(AppDataState state, LoadAppDataAction _) =>
            state with { IsLoading = true };

        [ReducerMethod]
        public static AppDataState ReduceLoadTodosSuccessAction(AppDataState state, LoadAppDataSuccessAction action) =>
            state with
            {
                Folders = action.Folders,
                Tags = action.Tags,
                IsLoading = false,
                CurrentErrorMessage = string.Empty,
            };

        [ReducerMethod]
        public static AppDataState ReduceLoadTodosFailureAction(AppDataState state, LoadAppDataFailureAction action) =>
            state with
            {
                IsLoading = false,
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
