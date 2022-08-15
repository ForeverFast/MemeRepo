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
                
            return source.Where(x => x.ParentFolderId == null).ToHashSet();
        }

        //public static HashSet<T> ToTree<T, F>(this IEnumerable<T> source,
        //    Func<T, F> keyFunc,
        //    Func<T, F> parentKeyFunc,
        //    Func<T, List<T>> childFunc,
        //    Func<T, T, T> parentFunc) where T : class
        //{
        //    var childsHash = source.ToLookup(parentKeyFunc);

        //    foreach (var cat in source)
        //    {
        //        childFunc(cat).ToList().Clear();
        //        foreach (var folder in childsHash[keyFunc(cat)])
        //        {
        //            parentFunc(folder)
        //            var parent = parentFunc.Invoke();

        //            parent = cat;

        //            childFunc(cat).Add(folder);
        //        }
        //    }

        //    return source.Where(x => x.ParentFolderId == null).ToHashSet();
        //}
    }
}
