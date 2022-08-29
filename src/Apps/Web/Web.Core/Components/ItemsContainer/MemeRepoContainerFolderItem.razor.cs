using Web.Core.Models.Components;

namespace Web.Core.Components.ItemsContainer
{
    public partial class MemeRepoContainerFolderItem
    {
        #region Params

        [Parameter] public MemeRepoItemViewModel Data { get; set; } = new MemeRepoItemViewModel();
        [Parameter] public EventCallback<MemeRepoItemViewModel> OnClickAction { get; set; } 

        #endregion

        #region Injects

        [Inject] NavigationManager? _navigationManager { get; init; }

        #endregion

        #region Internal events

        private async Task OnClick()
        {
            await OnClickAction.InvokeAsync(Data);
        }

        private void OnDoubleClick() => _navigationManager!.NavigateToFolder(Data.Id);

        #endregion
    }
}
