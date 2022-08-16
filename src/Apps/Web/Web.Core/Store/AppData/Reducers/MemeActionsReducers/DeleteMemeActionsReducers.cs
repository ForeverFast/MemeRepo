using System.Linq;
using Web.Core.Store.AppData.Actions.MemeActions.DeleteMemeActions;

namespace Web.Core.Store.AppData.Reducers.MemeActionsReducers
{
    internal static class DeleteMemeActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderAction(AppDataState state, DeleteMemeAction _)
            => state with
            {
            };

        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderSuccessAction(AppDataState state, DeleteMemeSuccessAction action)
        {
            var targetMeme = state.Items.First(x => x.Id == action.DeletedFolderId);
            state.Items.Remove(targetMeme);
            
            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceDeleteFolderFailureAction(AppDataState state, DeleteMemeFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
