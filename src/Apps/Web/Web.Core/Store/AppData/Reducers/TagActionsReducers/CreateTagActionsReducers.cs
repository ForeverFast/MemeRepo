using Web.Core.Store.AppData.Actions.TagActions.CreateTagActions;

namespace Web.Core.Store.AppData.Reducers.TagActionsReducers
{
    internal static class CreateTagActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceCreateFolderAction(AppDataState state, CreateTagAction _)
            => state with { };

        [ReducerMethod]
        public static AppDataState ReduceCreateFolderSuccessAction(AppDataState state, CreateTagSuccessAction action)
        {
            state.Tags.Add(action.NewTag);
            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceCreateFolderFailureAction(AppDataState state, CreateTagFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
