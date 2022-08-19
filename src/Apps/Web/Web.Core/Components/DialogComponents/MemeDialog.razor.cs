﻿using Domain.Core.Interfaces;
using Web.Core.Models;
using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.AppData;

namespace Web.Core.Components.DialogComponents
{
    public partial class MemeDialog
    {
        #region Params

        [CascadingParameter] public MudDialogInstance? MudDialog { get; set; }
        [Parameter] public MemeDialogViewModel Meme { get; set; } = new();



        public static DialogOptions DialogOptions = new DialogOptions
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            CloseButton = true,
        };

        #endregion

        #region Injects

        [Inject] IState<AppDataState>? _appDataState { get; init; }
        [Inject] IFileStorageProvider? _fileStorageProvider { get; init; }
        [Inject] IFileStorageDialogProvider? _fileStorageDialogProvider { get; init; }

        #endregion

        #region UI Fields

        private bool _isEditMode => Meme.Id != Guid.Empty;
        private string saveButtonText => _isEditMode ? "Сохранить" : "Создать";
        private string absoluteTmpFilePath = string.Empty;

        private List<TagViewModel> AllTags => _appDataState!.Value.Tags;
        private List<TagViewModel> CurrentFolderTags = new();
        private List<TagViewModel> SelectedFolderTags = new();

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            if (Meme.Id != Guid.Empty)
            {
                Meme.Path = _appDataState!.Value.GetFileRelativePath(Meme.ParentFolderId, Meme.Path);
            }

            base.OnInitialized();
        }

        #endregion

        protected void ChooseFile()
        {
            var absoluteFilePath = _fileStorageDialogProvider!.OpenFilePicker();
            if (string.IsNullOrEmpty(absoluteFilePath))
                return;

            absoluteTmpFilePath = _fileStorageProvider!.CopyFileToTmp(absoluteFilePath);
            Meme.Path = _fileStorageProvider!.GetRelativePath(absoluteTmpFilePath);
        }

        protected void Cancel() => MudDialog!.Cancel();

        protected void Save()
        {
            Meme.Path = absoluteTmpFilePath ?? string.Empty;
            MudDialog!.Close(DialogResult.Ok(Meme));
        }
    }
}
