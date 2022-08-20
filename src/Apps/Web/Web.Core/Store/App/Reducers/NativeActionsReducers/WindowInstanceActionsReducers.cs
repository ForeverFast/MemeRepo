using Web.Core.Store.App.Actions.NativeActions;

namespace Web.Core.Store.App.Reducers.NativeActionsReducers
{
    internal static class WindowInstanceActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceShowFileDropBlockAction(AppState state, WindowStateChangedAction action)
          => state with { AppWindowState = action.AppWindowState, };
    }
}
