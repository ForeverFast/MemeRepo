namespace Web.Core.Interfaces.Services
{
    public delegate Task NativeAppEvent();
    public interface INativeViewService
    {
        event NativeAppEvent CloseAppRequest;
        event NativeAppEvent MinimizeAppRequest;

        Task CloseApp();
        Task MinimazeApp();
    }
}
