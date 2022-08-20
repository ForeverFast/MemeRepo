using Web.Core.Store.App.Actions.TagActions.CreateTagActions;

namespace Web.Core.Store.App.Reducers.TagActionsReducers
{
    internal static class CreateTagActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceCreateFolderAction(AppState state, CreateTagAction _)
            => state with { };

        [ReducerMethod]
        public static AppState ReduceCreateFolderSuccessAction(AppState state, CreateTagSuccessAction action)
        {
            state.Tags.Add(action.NewTag);
            return state;
        }

        [ReducerMethod]
        public static AppState ReduceCreateFolderFailureAction(AppState state, CreateTagFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
