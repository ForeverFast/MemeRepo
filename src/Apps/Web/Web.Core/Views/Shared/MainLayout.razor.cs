using Fluxor.Blazor.Web.Components;
using MudBlazor.Utilities;
using Web.Core.Enums.Components.StateContainer;
using Web.Core.Services;
using Web.Core.Store.AppData.Actions.DataActions.AddFilesFromDiskActions;
using Web.Core.Store.AppData.Actions.DataActions.LoadAppDataActions;

namespace Web.Core.Views.Shared
{
    public partial class MainLayout : FluxorLayout
    {
        #region Params

        [CascadingParameter(Name = "IsMobileApp")] public bool IsMobileApp { get; set; }

        #endregion

        #region Injects

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

        protected virtual string SideMenuClassname => new CssBuilder("main-container__side-menu d-flex flex-column align-items-center")
            .AddClass("mobile-app", IsMobileApp)
            .AddClass("active", isShellOpen)
            .Build();

        protected virtual string ContentClassname => new CssBuilder("main-container__content")
            //.AddClass("folder-page", _navigationManager!.IsCurrentUrlFolder())
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

        protected void OnSwipe(SwipeDirection swipeDirection)
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

        protected void Test()
        {
            GC.Collect(); 
            GC.WaitForPendingFinalizers(); 
            //_dispatcher!.Dispatch(new AddFilesFromDiskAction(null));
        }
    }
}
