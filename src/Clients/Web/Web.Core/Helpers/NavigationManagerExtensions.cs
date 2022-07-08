using Domain.Core.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Core.Models;

namespace Web.Core.Helpers
{
    internal static class NavigationManagerExtensions
    {
        private static readonly string folderUrlPattern
            = @"^(\S*\/folder\/([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})\S*)$";

        public static void NavigateToFolder(this NavigationManager navigationManager, Guid FolderId)
            => navigationManager.NavigateTo($"folder/{FolderId}");

        public static bool IsCurrentUrlFolder(this NavigationManager navigationManager) => navigationManager.Uri.Contains("/folder/");

        public static bool IsCurrentUrlFolder(this NavigationManager navigationManager, out Guid? folderId)
        {
            var result = navigationManager.IsCurrentUrlFolder();
            if (result)
            {
                var matchValue = Regex.Match(navigationManager.Uri, folderUrlPattern);
                var tmp = matchValue.Groups[2].Value;
                tmp = tmp.Replace("/folder/", "");
                folderId = Guid.Parse(tmp);
            }
            else
            {
                folderId = null;
            }

            return result;
        }

        public static List<FolderTreeViewModel> FindFolderPathBreadcrumb(this HashSet<FolderTreeViewModel> treeRootList, Guid targetFolderId)
        {
            try
            {
                var allNodes = treeRootList.SelectRecursive(x => x.InnerFolders);
                var result = new List<FolderTreeViewModel>();
                var currentFolderId = targetFolderId;

                do
                {
                    var currentFolder = allNodes.FirstOrDefault(x => x.Id == currentFolderId);

                    if (currentFolder == null)
                        break;

                    result.Add(currentFolder);

                    if (!currentFolder.ParentFolderId.HasValue)
                        break;

                    currentFolderId = currentFolder.ParentFolderId.Value;
                }
                while (true);

                result.Reverse();

                return result;
            }
            catch (Exception ex)
            {
                return new();
            }

        }
    }
}
