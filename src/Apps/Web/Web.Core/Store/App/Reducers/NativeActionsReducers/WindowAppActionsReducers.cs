using Web.Core.Store.App.Actions.NativeActions.WindowAppActions;

namespace Web.Core.Store.App.Reducers.NativeActionsReducers
{
    internal static class WindowAppActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceShowFileDropBlockAction(AppState state, ShowFileDropBlockAction _)
          => state with { ShowFileDropBlock = true, };

        [ReducerMethod]
        public static AppState ReduceHideFileDropBlockAction(AppState state, HideFileDropBlockAction _)
           => state with { ShowFileDropBlock = false, };
    }
}
