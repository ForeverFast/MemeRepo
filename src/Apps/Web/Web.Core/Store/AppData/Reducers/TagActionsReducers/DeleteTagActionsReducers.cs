using Web.Core.Store.AppData.Actions.TagActions.DeleteTagActions;

namespace Web.Core.Store.AppData.Reducers.TagActionsReducers
{
    internal static class DeleteTagActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderAction(AppDataState state, DeleteTagAction _)
            => state with
            {
            };

        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderSuccessAction(AppDataState state, DeleteTagSuccessAction action)
        {
            var targetFolder = state.Tags.FirstOrDefault(x => x.Id == action.DeletedTagId);

            if (targetFolder != null)
                state.Tags.Remove(targetFolder);    

            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderFailureAction(AppDataState state, DeleteTagFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
