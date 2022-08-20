using Domain.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Web.Core.Store.App;

namespace Web.Core.Utils.WebScopeManager
{
    public class WebScopeManager
    {
        #region Injects

        private readonly IFileStorageProvider _fileStorageProvider;

        #endregion

        #region Ctors

        public WebScopeManager(IFileStorageProvider fileStorageProvider)
        {
            _fileStorageProvider = fileStorageProvider;
        }

        #endregion

        #region Fields

        private IState<AppState>? appDataState;
        private IDispatcher? dispatcher;
        private IActionSubscriber? actionSubscriber;

        #endregion

        #region Props

        public Guid? CurrentContentId => appDataState?.Value?.CurrentContentId;
        public IDispatcher Dispatcher => dispatcher ?? throw OnNotInitializedException();
        public IActionSubscriber ActionSubscriber => actionSubscriber ?? throw OnNotInitializedException();

        #endregion

        #region Events

        public event EventHandler OnInitialized;

        #endregion

        private Exception OnNotInitializedException() => new Exception("Not initialized");

        public ValueTask Init(IServiceProvider serviceProvider)
        {
            appDataState = serviceProvider.GetRequiredService<IState<AppState>>();
            dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
            actionSubscriber = serviceProvider.GetRequiredService<IActionSubscriber>();

            _fileStorageProvider.DeleteFolder(_fileStorageProvider.TmpPath);

            OnInitialized?.Invoke(this, new EventArgs { });

            return ValueTask.CompletedTask;
        }
    }
}
