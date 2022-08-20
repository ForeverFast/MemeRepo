using Web.Core.Store.App.Actions.DataActions.SetCurrentContentActions;

namespace Web.Core.Store.App.Reducers.DataActionsReducers
{
    internal static class SetCurrentContentActionsReducers
    {
        [ReducerMethod]
        public static AppState ReduceSetCurrentFolderAction(AppState state, SetCurrentContentAction _)
           => state with { };

        [ReducerMethod]
        public static AppState ReduceSetCurrentContentSuccessAction(AppState state, SetCurrentContentSuccessAction action)
        {
            if (action.CurrentContentType == ContentType.FolderPage)
            {
                var oldFolderPath = state.FolderPath;
                var newFolderPath = state.FolderList.First(x => x.Id == action.CurrentContentId)
                    .SelectRecursive(x => x.ParentFolder)
                    .Reverse()
                    .ToHashSet();

                foreach (var folder in oldFolderPath)
                {
                    if (folder.IsExpanded && !folder.HasChild)
                        folder.IsExpanded = false;
                    folder.PathFlag = false;
                }

                foreach (var folder in newFolderPath)
                    folder.PathFlag = true;

                state = state with
                {
                    FolderPath = newFolderPath,
                };
            }
            else
            {
                foreach (var folder in state.FolderPath)
                {
                    if (folder.IsExpanded && !folder.HasChild)
                        folder.IsExpanded = false;
                    folder.PathFlag = false;
                }
            }

            action.Items.ForEach(x =>
            {
                switch (x.FolderObjectType)
                {
                    case Enums.Components.MemeRepoItemType.Folder:
                        break;
                    case Enums.Components.MemeRepoItemType.Img:
                    default:
                        var abslPath = state.GetFileRelativePath(x.ParentFolderId!.Value, x.Path);
                        x.Path = abslPath;//"data:image/png;base64, " + Convert.ToBase64String(File.ReadAllBytes(abslPath));
                        break;
                }
            });

            return state with
            {
                CurrentContentType = action.CurrentContentType,
                CurrentContentId = action.CurrentContentId,
                Items = action.Items,
            };
        }

        [ReducerMethod]
        public static AppState ReduceSetCurrentContentFailureAction(AppState state, SetCurrentContentFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
