using Domain.Core.Interfaces;
using Web.Core.Enums.Components.StateContainer;
using Web.Core.Interfaces.Services.ViewServices;
using Web.Core.Models.Components;
using Web.Core.Services.ViewServices;
using Web.Core.Store.AppData;
using Web.Core.Store.AppData.Actions.ChangeStateActionsEffects;

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
        [Inject] IFileStorageProvider? _nativeFileStorageProvider { get; init; }
        [Inject] IFileStorageDialogProvider? _fileStorageDialogProvider { get; init; }
        [Inject] IDataViewService? _dataViewService { get; init; }  

        [Inject] IMapper? _mapper { get; init; }  

        #endregion

        #region UI Fields

        protected Guid _currentFolderId { get; set; }
        protected List<MemeRepoItemViewModel> FolderObjects = new List<MemeRepoItemViewModel>();
        protected List<BreadcrumbItem> FolderBreadcrumbItems => new(); // SelectedFolderPath.SelectToList(x => new BreadcrumbItem(x.Title, $"{x.Id}"));

        #endregion

        #region State methods

        protected override async Task OnParametersSetAsync()
        {
            State = ComponentState.Loading;
            this.StateHasChanged();

            if (_currentFolderId != Id)
            {
                _currentFolderId = Id;

                FolderObjects.Clear();
                var memes = await _dataViewService!.LoadFolderMemesByFolderId(_currentFolderId);
                var folderItems = _mapper!.Map<List<MemeRepoItemViewModel>>(memes);
                folderItems.ForEach(x => x.Path = _appState!.Value.GetFileRelativePath(_currentFolderId, x.Path));
                FolderObjects = folderItems;
            }

            State = ComponentState.Content;

            this.StateHasChanged();

            _dispatcher!.Dispatch(new SetCurrentFolderAction
            {
                Folder = _appState!.Value.FolderList.First(x => x.Id == _currentFolderId)
            });
        }

        #endregion

        #region Internal Events

        protected void OnFolderBreadcrumbItemClick(string folderId) => _navigationManager!.NavigateToFolder(Guid.Parse(folderId));

        #endregion
    }
}
