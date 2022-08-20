using Web.Core.Store.App.Actions.TagActions.DeleteTagActions;

namespace Web.Core.Store.App.Reducers.TagActionsReducers
{
    internal static class DeleteTagActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceDeleteFolderAction(AppState state, DeleteTagAction _)
            => state with
            {
            };

        [ReducerMethod]
        public static AppState ReduceDeleteFolderSuccessAction(AppState state, DeleteTagSuccessAction action)
        {
            var targetFolder = state.Tags.FirstOrDefault(x => x.Id == action.DeletedTagId);

            if (targetFolder != null)
                state.Tags.Remove(targetFolder);    

            return state;
        }

        [ReducerMethod]
        public static AppState ReduceDeleteFolderFailureAction(AppState state, DeleteTagFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
