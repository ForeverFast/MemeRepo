using FluentValidation;
using Web.Core.Models;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.App;
using Web.Core.Utils;

namespace Web.Core.Components.DialogComponents
{
    public partial class TagDialog
    {
        #region Params

        [CascadingParameter] public MudDialogInstance? MudDialog { get; set; }
        [Parameter] public TagDialogViewModel Tag { get; set; } = new();



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

        private bool _isEditMode => Tag.Id != Guid.Empty;
        private string saveButtonText => _isEditMode ? "Сохранить" : "Создать";

        private List<TagViewModel> AllTags => _appState!.Value.Tags;

        #endregion

        #region Validation

        FluentValueValidator<string> titleValidator => new FluentValueValidator<string>(x => x
        .NotEmpty()
        .NotNull()
        .Custom((val,ctx) =>
        {
            if (AllTags.Any(x => x.Title == val))
                ctx.AddFailure("Title", "Такой тег уже существует!");
        }));

        #endregion

        protected void Cancel() => MudDialog!.Cancel();

        protected void Save() => MudDialog!.Close(DialogResult.Ok(Tag));
    }
}
