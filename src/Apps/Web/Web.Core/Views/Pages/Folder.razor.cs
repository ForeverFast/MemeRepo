using Domain.Core.Interfaces;
using Web.Core.Enums.Components.StateContainer;
using Web.Core.Models.Components;
using Web.Core.Store.AppData;

namespace Web.Core.Views.Pages
{
    [Route("folder/{Id:guid}")]
    public partial class Folder
    {
        #region Params

        [Parameter] public Guid Id { get; set; }

        #endregion

        #region Injects

        [Inject] IState<AppDataState>? _appState { get; init; }
        [Inject] IDispatcher? _dispatcher { get; init; }

        [Inject] NavigationManager? _navigationManager { get; init; }

        [Inject] IMediator? _mediator { get; init; }

        [Inject] IFileStorageProvider? _nativeFileStorageProvider { get; init; }
        [Inject] IFileStorageDialogProvider? _fileStorageDialogProvider { get; init; }

        #endregion

        #region UI Fields

        protected Guid _currentFolderId { get; set; }
        protected List<MemeRepoItemViewModel> FolderObjects = new List<MemeRepoItemViewModel>();
        protected List<BreadcrumbItem> FolderBreadcrumbItems => new(); // SelectedFolderPath.SelectToList(x => new BreadcrumbItem(x.Title, $"{x.Id}"));

        #endregion

        #region Internal Events

        protected void OnFolderBreadcrumbItemClick(string folderId) => _navigationManager!.NavigateToFolder(Guid.Parse(folderId));

        #endregion

        


        protected override async Task OnParametersSetAsync()
        {
            State = ComponentState.Loading;
            this.StateHasChanged();

            if (_currentFolderId != Id)
            {
                FolderObjects.Clear();

            }

            State = ComponentState.Content;
        }


        private string GetImageAnotherName(string parentFolderPath, string imagePath)
        {
            string newMemePath = @$"{parentFolderPath}\{Path.GetFileName(imagePath)}";
            if (!File.Exists(newMemePath))
            {
                return newMemePath;
            }

            int num = 2;
            while (true)
            {
                newMemePath = @$"{parentFolderPath}\{Path.GetFileNameWithoutExtension(imagePath)} ({num++}){Path.GetExtension(imagePath)}";
                if (!File.Exists(newMemePath))
                {
                    return newMemePath;
                }
            }
        }


        async Task OpenFolderPicker()
        {
            try
            {
                //await _bsm!.Folders.CreateFolderAsync(new FolderDto
                //{
                //    Title = "test folder",
                //});

                //var result = _nativeFileStorageProvider.OpenFolderPicker();


            }
            catch (Exception ex)
            {

            }
        }

        void OpenFilePicker()
        {
            try
            {
                //if (!Directory.Exists(parentFolderPath))
                //    Directory.CreateDirectory(parentFolderPath);

                //var imagePath = _fileStorageDialogProvider.OpenFilePicker();
                //string title = null;
                //string newImagePath = string.Empty;
                //if (!string.IsNullOrEmpty(title))
                //{
                //    newImagePath = Path.Combine(parentFolderPath, $@"{title}{Path.GetExtension(imagePath)}");
                //}
                //else
                //{
                //    newImagePath = Path.Combine(parentFolderPath, Path.GetFileName(imagePath));
                //}

                //newImagePath = GetImageAnotherName(parentFolderPath, newImagePath);
                //File.Copy(imagePath, newImagePath);

                //var correctPath = newImagePath.Replace(parentFolderPath + "\\", "\\user-files\\");
                //var obj = new MemeRepoItemViewModel
                //{
                //    Title = "Object 1",
                //    Path = correctPath
                //};


                //for (int i = 0; i < 150; i++)
                //    FolderObjects.Add(obj);

                this.StateHasChanged();
            }
            catch (Exception ex)
            {

            }
        }

        void Test()
        {
            try
            {
                //_nativeFileStorageProvider.OpenFolderPicker();

                //Directory.CreateDirectory("D:\\test");
                //using var file = File.Create($"D:\\test.txt");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
