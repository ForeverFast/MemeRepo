using Web.Core.Store.AppData.Actions.ChangeStateActionsEffects;

namespace Web.Core.Store.AppData.Reducers
{
    internal static class ChangeStateActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceSetCurrentFolderAction(AppDataState state, SetCurrentFolderAction action)
            => state with 
            {
                CurrentFolder = action.Folder, 
            };
    }
}
