using Domain.Core.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;

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
    }
}
