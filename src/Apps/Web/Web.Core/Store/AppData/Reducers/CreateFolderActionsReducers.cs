using Domain.Core.Extensions;
using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.AppData.Reducers
{
    internal static class CreateFolderActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceLoadTodosAction(AppDataState state, CreateFolderAction _) =>
           state with { };

        [ReducerMethod]
        public static AppDataState ReduceLoadTodosSuccessAction(AppDataState state, CreateFolderSuccessAction action)
        {
            if (!action.NewFolder.ParentFolderId.HasValue)
            {
                state.Folders.Add(action.NewFolder);
                return state;
            }

            var parentFolder = state.Folders.SelectManyRecursive(x => x.Folders).First(x => x.Id == action.NewFolder.ParentFolderId);
            parentFolder.Folders.Add(action.NewFolder);    
            return state;
        }

        [ReducerMethod]
        public static AppDataState ReduceLoadTodosFailureAction(AppDataState state, CreateFolderFailureAction action) =>
            state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
