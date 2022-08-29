using Domain.Core.Interfaces;
using Web.Core.Models.Components;

namespace Web.Core.Components.ItemsContainer
{
    public partial class MemeRepoContainerMemeItem
    {
        #region Params

        [Parameter] public MemeRepoItemViewModel Data { get; set; } = new MemeRepoItemViewModel();
        [Parameter] public EventCallback<MemeRepoItemViewModel> OnClickAction { get; set; }

        #endregion

        #region Injects

        [Inject] ISnackbar? _snackbar { get; init; }
        [Inject] IFileStorageProvider? _fileStorageProvider { get; init; }


        #endregion

        #region Internal events

        private async Task OnClick()
        {
            _fileStorageProvider!.CopyFileToClipboard(Data.Path);
            _snackbar!.Add("Скопировано", Severity.Info, opt =>
            {
                opt.VisibleStateDuration = 1000;
            });

            await OnClickAction.InvokeAsync(Data);
        }

        private void OnDoubleClick() => _fileStorageProvider!.OpenFile(Data.Path);

        #endregion
    }
}
