using Web.Core.Models.Components;

namespace Web.Core.Helpers
{
    public static class TreeViewHelper
    {
        public static HashSet<FolderTreeViewModel> ToTree(this IEnumerable<FolderTreeViewModel> source)
        {
            var childsHash = source.ToLookup(cat => cat.ParentFolderId);

            foreach (var cat in source)
            {
                cat.Folders.Clear();
                foreach (var folder in childsHash[cat.Id])
                {
                    folder.ParentFolder = cat;
                    cat.Folders.Add(folder);
                }
            }
                
            return source.ToHashSet();
        }
    }
}
