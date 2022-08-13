using Web.Core.Models.Components;

namespace Web.Core.Components.DialogComponents
{
    public partial class FolderDialog
    {
        public static DialogOptions DialogOptions = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = true,
        };

        #region Params

        [CascadingParameter] public MudDialogInstance? MudDialog { get; set; }
        [Parameter] public FolderDialogViewModel Folder { get; set; } = new();

        #endregion

        protected void Cancel() => MudDialog!.Cancel();

        protected void Create() => MudDialog!.Close(DialogResult.Ok(Folder));
    }
}
