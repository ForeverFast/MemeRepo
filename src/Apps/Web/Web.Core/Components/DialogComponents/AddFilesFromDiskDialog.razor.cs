using Domain.Core.Interfaces;
using Web.Core.Models.Components.Dialogs;

namespace Web.Core.Components.DialogComponents
{
    public partial class AddFilesFromDiskDialog
    {
        #region Params

        [CascadingParameter] public MudDialogInstance? MudDialog { get; set; }



        public static DialogOptions DialogOptions = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = true,
        };

        #endregion

        #region Injects

        [Inject] IFileStorageDialogProvider? _fileStorageDialogProvider { get; init; }

        #endregion

        #region UI Fields

        private List<PathsPackViewModel> folders = new();
        private List<PathsPackViewModel> memes = new();

        #endregion

        #region Internal events

        private void AddFolders()
        {
            var pack = _fileStorageDialogProvider!.OpenMultiselectPicker(true);
            if (pack != null)
                folders.Add(new PathsPackViewModel { Paths = pack });
        }

        private void RemoveFolderPack(PathsPackViewModel pack) => folders.Remove(pack);

        private void AddMemes()
        {
            var pack = _fileStorageDialogProvider!.OpenMultiselectPicker(false);
            if (pack != null)
                memes.Add(new PathsPackViewModel { Paths = pack });
        }

        private void RemoveMemePack(PathsPackViewModel pack) => memes.Remove(pack);

        private void Cancel() => MudDialog!.Cancel();

        private void Confirm()
        {
            var folderPaths = folders.SelectMany(x => x.Paths.Select(x => x));
            var memePaths = memes.SelectMany(x => x.Paths.Select(x => x));

            var result = folderPaths.Concat(memePaths);

            MudDialog!.Close(DialogResult.Ok(result));
        }

        #endregion
    }
}
