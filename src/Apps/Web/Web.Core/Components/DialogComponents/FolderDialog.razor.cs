using Web.Core.Models;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.App;

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

        [Inject] IState<AppState>? _appState { get; init; }

        #endregion

        #region UI Fields

        private bool _isEditMode => Folder.Id != Guid.Empty;
        private string saveButtonText => _isEditMode ? "Сохранить" : "Создать";

        private List<ItemTagViewModel> selectedTags = new();

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

        protected void Save() 
        {
            Folder.Tags = selectedTags.Select(x => x.Id).ToList();
            MudDialog!.Close(DialogResult.Ok(Folder));
        } 
    }
}
