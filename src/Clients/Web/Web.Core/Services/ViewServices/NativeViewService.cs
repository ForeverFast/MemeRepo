using Web.Core.Interfaces.Services.ViewServices;

namespace Web.Core.Services.ViewServices
{
    internal class NativeViewService : INativeViewService
    {
        public event NativeAppEvent? CloseAppRequest;
        public event NativeAppEvent? MinimizeAppRequest;


        public Task CloseApp() => CloseAppRequest?.Invoke() ?? Task.CompletedTask;

        public Task MinimazeApp() => MinimizeAppRequest?.Invoke() ?? Task.CompletedTask;
    }
}
