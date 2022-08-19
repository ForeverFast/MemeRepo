using Domain.Core.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hybrid.WindowsApp.Implementations
{
    internal class WindowsDialogProvider : IFileStorageDialogProvider
    {
        public void ShowFolder(string path)
        {
            using (var proc = Process.Start("explorer", path)) { }
        }

        public string OpenFilePicker(string? extension = null)
        {
            extension ??= "*.jpg;*.png";

            var dlg = new CommonOpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filters.Add(new CommonFileDialogFilter("Файлы", extension));
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileName;
            else
                return string.Empty;
        }

        public string OpenFolderPicker()
        {
            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileName;
            else
                return string.Empty;
        }

        public IEnumerable<string> OpenMultiselectPicker(bool folderPickerMode = false)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Multiselect = true;
            dlg.IsFolderPicker = folderPickerMode;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileNames;
            else
                return Enumerable.Empty<string>();
        }
    }
}
