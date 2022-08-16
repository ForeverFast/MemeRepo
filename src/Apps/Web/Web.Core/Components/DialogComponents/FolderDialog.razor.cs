using Web.Core.Models;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.AppData;

namespace Web.Core.Components.DialogComponents
{
    public partial class FolderDialog
    {
        #region Params

        [CascadingParameter] public MudDialogInstance? MudDialog { get; set; }
        [Parameter] public FolderDialogViewModel Folder { get; set; } = new();



        public static DialogOptions DialogOptions = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = true,
        };

        #endregion

        #region Injects

        [Inject] IState<AppDataState>? _appDataState { get; init; }

        #endregion

        #region UI Fields

        private bool _isEditMode => Folder.Id != Guid.Empty;

        private List<TagViewModel> AllTags => _appDataState!.Value.Tags;
        private List<TagViewModel> CurrentFolderTags = new();
        private List<TagViewModel> SelectedFolderTags = new();

        #endregion

        #region State methods

        protected override void OnParametersSet()
        {
            if (Folder.Id != Guid.Empty)
            {

            }
        }

        #endregion

        protected void Cancel() => MudDialog!.Cancel();

        protected void Save() => MudDialog!.Close(DialogResult.Ok(Folder));
    }
}
