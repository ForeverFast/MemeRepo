﻿namespace Domain.Core.Interfaces
{
    public interface IFileStorageDialogProvider
    {
        void ShowFolder(string path);
        string OpenFilePicker(string? extension = null);
        string OpenFolderPicker();
    }
}
