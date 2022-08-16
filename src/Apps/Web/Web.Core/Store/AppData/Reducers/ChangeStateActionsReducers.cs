﻿using Web.Core.Enums;
using Web.Core.Store.AppData.Actions.ChangeStateActions;

namespace Web.Core.Store.AppData.Reducers
{
    internal static class ChangeStateActionsReducers
    {
        [ReducerMethod]
        public static AppDataState ReduceSetCurrentFolderAction(AppDataState state, SetCurrentContentAction _)
            => state with { };

        [ReducerMethod]
        public static AppDataState ReduceSetCurrentContentSuccessAction(AppDataState state, SetCurrentContentSuccessAction action)
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
                        x.Path = state.GetFileRelativePath(x.ParentFolderId!.Value, x.Path);
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
        public static AppDataState ReduceSetCurrentContentFailureAction(AppDataState state, SetCurrentContentFailureAction action)
            => state with
            {
                CurrentErrorMessage = action.ErrorMessage
            };
    }
}
