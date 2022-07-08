using Microsoft.AspNetCore.Components;
using MudBlazor;
using Web.Core.Models;
using Web.Core.Helpers;
using Domain.Core.Extensions;
using Web.Core.Services.ViewServices;

namespace Web.Core.Views.Shared
{
    public partial class MainLayout
    {
        #region Params

        [CascadingParameter(Name = "IsMobileApp")] public bool IsMobileApp { get; set; } 

        #endregion

        #region Injects

        [Inject] NavigationManager _navigationManager { get; init; }
        [Inject] ThemeService _themeService { get; init; }

        #endregion

        #region UI Fields/Props

        protected bool _isPageLoaded = false;
        protected bool _isFolderView = true;
        protected bool _isShellOpen = true;

        protected List<BreadcrumbItem> _breadcrumbItems;

        protected HashSet<FolderTreeViewModel> FolderTree = new();
        protected HashSet<FolderTreeViewModel> ExpandedNodes = new();
        protected FolderTreeViewModel SelectedFolder;

        #endregion

        #region State methods

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                var f1 = Guid.Parse("8764fe21-3036-43ce-a51d-077fcab7408b");
                var f2 = Guid.Parse("8a2f8855-8da5-48a9-9fee-92d835aa4b17");
                var f3 = Guid.Parse("551ebe23-0efb-4899-b819-0b8f0f50ae7b");
                var f4 = Guid.Parse("e631c6aa-e9f1-4063-bee3-48a95b5f4a18");
                var f21 = Guid.Parse("4f637863-bae3-4059-88d2-bdc7478d0881");
                var f22 = Guid.Parse("fa2686f0-ced9-4b8b-8a6f-2e4bd10df6b5");
                var f23 = Guid.Parse("95721b88-0560-4d38-adfe-32f9f928feff");
                var f24 = Guid.Parse("7d9e777b-0ba6-46f3-8631-633a73d106e5");
                var f221 = Guid.Parse("16c3d883-ffaf-44e1-a05c-469310190e79");
                var f222 = Guid.Parse("ca50a5e3-3a17-4708-bc2a-699463ab7298");
                var f223 = Guid.Parse("03e8081a-b0a1-4097-a50f-b8e91a7263c6");
                var f224 = Guid.Parse("ec554ff5-c315-45d7-b1f1-c33edb9bccf7");

                try
                {
                    FolderTree = new HashSet<FolderTreeViewModel>
                    {
                         new FolderTreeViewModel { Title = "Folder 1", Id = f1 },
                        new FolderTreeViewModel { Title = "Folder 2", Id = f2,
                            InnerFolders = new()
                            {
                                new FolderTreeViewModel { Title = "Folder 2.1", Id = f21, ParentFolderId = f2 },
                                new FolderTreeViewModel { Title = "Folder 2.2", Id = f22, ParentFolderId = f2,
                                    InnerFolders = new()
                                    {
                                        new FolderTreeViewModel { Title = "Folder 2.2.1", Id = f221, ParentFolderId = f22 },
                                        new FolderTreeViewModel { Title = "Folder 2.2.2", Id = f222, ParentFolderId = f22 },
                                        new FolderTreeViewModel { Title = "Folder 2.2.3", Id = f223, ParentFolderId = f22 },
                                        new FolderTreeViewModel { Title = "Folder 2.2.4", Id = f224, ParentFolderId = f22 }
                                    }
                                },
                                new FolderTreeViewModel { Title = "Folder 2.3", Id = f23, ParentFolderId = f2 },
                                new FolderTreeViewModel { Title = "Folder 2.4", Id = f24, ParentFolderId = f2 }
                            },
                        },
                        new FolderTreeViewModel { Title = "Folder 3", Id = f3 },
                        new FolderTreeViewModel { Title = "Folder 4", Id = f4 },
                    };

                    if (_navigationManager.IsCurrentUrlFolder(out var folderId))
                    {
                        SelectedFolder = FolderTree.SelectRecursive(x => x.InnerFolders).FirstOrDefault(x => x.Id == folderId.Value);
                        SelectedFolder.IsActivated = true;
                        GetFolderBreadcrumbItems();
                    }

                    _isPageLoaded = true;
                    this.StateHasChanged();
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion

        #region Folder logic

        protected void OnFolderTreeViewItemClick(FolderTreeViewModel context)
        {
            _navigationManager.NavigateToFolder(context.Id);

            if (SelectedFolder != null)
            {
                SelectedFolder.IsActivated = false;
            }
            
            SelectedFolder = context;
            SelectedFolder.IsActivated = true;
        }

        protected List<BreadcrumbItem> GetFolderBreadcrumbItems()
        {
            if (SelectedFolder != null)
            {
                _breadcrumbItems = new();
                var folders = FolderTree?.FindFolderPathBreadcrumb(SelectedFolder.Id);
                folders.ForEach(x =>
                {
                    if(!_isPageLoaded)
                        x.IsExpanded = true;
                    _breadcrumbItems.Add(new BreadcrumbItem(x.Title, $"folder/{x.Id}"));
                });
                var targetFolder = folders.Last();
                if (!_isPageLoaded)
                    targetFolder.IsExpanded = false;
                targetFolder.IsActivated = true;
            }
            return _breadcrumbItems;
        }

        #endregion

        protected string BuildTreeViewCssClass()
        {
            var result = string.Empty;
            
            result += IsMobileApp ? "mobile-app-treeview " : string.Empty;
            result += _isShellOpen ? "active " : string.Empty;

            return result;
        }

        protected void OnSwipe(SwipeDirection swipeDirection)
        {
            switch (swipeDirection)
            {
                case SwipeDirection.LeftToRight:
                    if (!_isShellOpen)
                    {
                        _isShellOpen = true;
                        this.StateHasChanged();
                    }
                    break;
                case SwipeDirection.RightToLeft:
                    if (_isShellOpen)
                    {
                        _isShellOpen = false;
                        this.StateHasChanged();
                    }
                    break;
                case SwipeDirection.None:
                case SwipeDirection.TopToBottom:
                case SwipeDirection.BottomToTop:
                default:
                    break;
            }
        }
    }
}
