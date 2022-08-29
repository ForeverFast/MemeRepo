using Microsoft.AspNetCore.Components.Web;
using System.Timers;
using Web.Core.Models.Components.Search;
using Timer = System.Timers.Timer;

namespace Web.Core.Components.GlobalSearch
{
    public partial class MemeRepoGlobalSearch : IDisposable
    {
        #region Injects

        [Inject] NavigationManager? _navigationManager { get; init; }

        #endregion

        #region UI Fields

        private string searchText = string.Empty;
        private string lastSearchedText = string.Empty;

        private Timer aTimer = default!;

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            aTimer = new Timer(750);
            aTimer.Elapsed += OnUserFinish;
            aTimer.AutoReset = false;

            base.OnInitialized();
        }

        public void Dispose()
        {
            aTimer?.Dispose();
        }

        #endregion

        #region Internal events

        private void ResetTimer(KeyboardEventArgs e)
        {
            aTimer.Stop();
            aTimer.Start();
        }

        private void OnUserFinish(Object? source, ElapsedEventArgs e)
        {
            if (lastSearchedText != searchText && !string.IsNullOrEmpty(searchText))
            {
                _navigationManager!.NavigateToSearch(new FilterModel
                {
                    SearchTitle = lastSearchedText = searchText,
                    // TODO
                    //Tags =
                });
            }
        }

        #endregion
    }
}
