using Domain.Core.Enums;

namespace Web.Core.Store.App.Actions.NativeActions
{
    public record MinimizeWindowAction();
    public record ResizeWindowAction();
    public record CloseWindowAction();

    public record WindowStateChangedAction(AppWindowState AppWindowState);
}
