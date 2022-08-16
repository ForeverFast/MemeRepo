using MudBlazor.Utilities;
using Web.Core.Enums.Components.StateContainer;
using Web.Core.Services;
using Web.Core.Store.AppData.Actions.LoadAppDataActions;

namespace Web.Core.Views.Shared
{
    public partial class MainLayout
    {
        #region Params

        [CascadingParameter(Name = "IsMobileApp")] public bool IsMobileApp { get; set; }

        [Parameter] public ComponentState State { get; set; } = ComponentState.Loading;

        #endregion

        #region Injects

        [Inject] IDispatcher? _dispatcher { get; init; }

        [Inject] NavigationManager? _navigationManager { get; init; }
        [Inject] ThemeService? _themeService { get; init; }

        #endregion

        #region UI Fields/Props

        protected bool _isPageLoaded = false;
        protected bool _isFolderView = true;
        protected bool _isShellOpen = true;

        #endregion

        #region Component Css/Style

        protected virtual string SideMenuClassname => new CssBuilder("main-container__side-menu d-flex flex-column align-items-center")
            .AddClass("mobile-app", IsMobileApp)
            .AddClass("active", _isShellOpen)
            .Build();

        protected virtual string ContentClassname => new CssBuilder("main-container__content")
            //.AddClass("folder-page", _navigationManager!.IsCurrentUrlFolder())
            .Build();

        #endregion

        protected override void OnInitialized()
        {
            _dispatcher!.Dispatch(new LoadAppDataAction());

            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            State = ComponentState.Content;

            base.OnParametersSet();
        }

        protected void OnSwipe(SwipeDirection swipeDirection)
        {
            switch (swipeDirection)
            {
                case SwipeDirection.LeftToRight:
                    if (!_isShellOpen)
                    {
                        _isShellOpen = true;
                        this.StateHasChanged();
                    }
                    break;
                case SwipeDirection.RightToLeft:
                    if (_isShellOpen)
                    {
                        _isShellOpen = false;
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
        }
    }
}
