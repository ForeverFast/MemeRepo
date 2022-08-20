using Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions;

namespace Web.Core.Store.App.Reducers.MemeActionsReducers
{
    internal static class DeleteMemeActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceDeleteFolderAction(AppState state, DeleteMemeAction _)
            => state with
            {
            };

        [ReducerMethod]
        public static AppState ReduceDeleteFolderSuccessAction(AppState state, DeleteMemeSuccessAction action)
        {
            var targetMeme = state.Items.First(x => x.Id == action.DeletedMemeId);
            state.Items.Remove(targetMeme);

            return state;
        }

        [ReducerMethod]
        public static AppState ReduceDeleteFolderFailureAction(AppState state, DeleteMemeFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
