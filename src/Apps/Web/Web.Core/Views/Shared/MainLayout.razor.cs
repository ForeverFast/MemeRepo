using Domain.Core.Enums;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Utilities;
using Web.Core.Enums.Components.StateContainer;
using Web.Core.Services;
using Web.Core.Store.App;
using Web.Core.Store.App.Actions.DataActions.LoadAppDataActions;
using Web.Core.Store.App.Actions.NativeActions;
using Web.Core.Store.App.Actions.NativeActions.WindowAppActions;

namespace Web.Core.Views.Shared
{
    public partial class MainLayout : MemeRepoBaseFluxorLayout
    {
        #region Params

        [CascadingParameter(Name = "IsMobileApp")] public bool IsMobileApp { get; set; }

        #endregion

        #region Injects

        [Inject] IState<AppState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }

        [Inject] NavigationManager? _navigationManager { get; init; }
        [Inject] ThemeService? _themeService { get; init; }

        #endregion

        #region UI Fields

        private ComponentState state = ComponentState.Loading;
        private bool isFolderView = true;
        private bool isShellOpen = true;

        #endregion

        #region Component Css/Style

        private string SideMenuClassname => new CssBuilder("main-container__side-menu d-flex flex-column align-items-center")
            .AddClass("mobile-app", IsMobileApp)
            .AddClass("active", isShellOpen)
            .Build();

        private string ContentClassname => new CssBuilder("main-container__content")
            //.AddClass("folder-page", _navigationManager!.IsCurrentUrlFolder())
            .Build();

        private string ResizeIconClassname => new CssBuilder()
            .AddClass("icon-resize-maximized", _appState!.Value.AppWindowState == AppWindowState.Maximized)
            .Build();

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            SubscribeToAction<LoadAppDataSuccessAction>(OnLoadAppDataSuccessAction);
            SubscribeToAction<LoadAppDataFailureAction>(OnLoadAppDataFailureAction);

            _dispatcher!.Dispatch(new LoadAppDataAction());

            base.OnInitialized();
        }

        #endregion

        #region External Events

        private void OnLoadAppDataSuccessAction(LoadAppDataSuccessAction action)
            => _ = InvokeAsync(() =>
            {
                state = ComponentState.Content;
                this.StateHasChanged();
            });

        private void OnLoadAppDataFailureAction(LoadAppDataFailureAction action)
           => _ = InvokeAsync(() =>
           {
               state = ComponentState.Error;
               this.StateHasChanged();
           });

        #endregion

        #region Internal events

        private void OnSwipe(SwipeDirection swipeDirection)
        {
            switch (swipeDirection)
            {
                case SwipeDirection.LeftToRight:
                    if (!isShellOpen)
                    {
                        isShellOpen = true;
                        this.StateHasChanged();
                    }
                    break;
                case SwipeDirection.RightToLeft:
                    if (isShellOpen)
                    {
                        isShellOpen = false;
                        this.StateHasChanged();
                    }
                    break;
                case SwipeDirection.None:
                case SwipeDirection.TopToBottom:
                case SwipeDirection.BottomToTop:
                default:
                    break;
            }
        }

        private void OnDragEnter(DragEventArgs e)
        {
            var inputDataTypes = e.DataTransfer.Types;
            if (!inputDataTypes.Except(new List<string> { "Files" }).Any() && inputDataTypes.Contains("Files"))
            {
                _dispatcher!.Dispatch(new ShowFileDropBlockAction { });
            }
        }
        private void OnDragLeave(DragEventArgs e) => _dispatcher!.Dispatch(new HideFileDropBlockAction { });

        private void OnMinimizeWindowButtonClick() => _dispatcher!.Dispatch(new MinimizeWindowAction());
        private void OnResizeWindowButtonClick() => _dispatcher!.Dispatch(new ResizeWindowAction());
        private void OnCloseWindowButtonClick() => _dispatcher!.Dispatch(new CloseWindowAction());

        private void Test()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //_dispatcher!.Dispatch();
        }

        #endregion
    }
}
