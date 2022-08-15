using DALQueryChain.Interfaces;
using Web.Core.Interfaces.Services.ViewServices;
using Web.Core.Models;
using Web.Core.Models.Components;
using Web.Core.Store.AppData;

namespace Web.Core.Services.ViewServices
{
    internal class DataViewService : IDataViewService
    {
        #region Injects

        private readonly IState<AppDataState> _appDataState;
        private readonly IDispatcher _dispatcher;
        private readonly IDialogService _dialogService;
        private readonly IMapper _mapper;
        private readonly IDALQueryChain<MemeRepoDbContext> _dal;

        #endregion

        #region Ctors

        public DataViewService(IDispatcher dispatcher,
                               IDialogService dialogService,
                               IMapper mapper,
                               IState<AppDataState> appDataState,
                               IDALQueryChain<MemeRepoDbContext> dal)
        {
            _dispatcher = dispatcher;
            _dialogService = dialogService;
            _mapper = mapper;
            _appDataState = appDataState;
            _dal = dal;
        }

        #endregion

        #region Folder

        public async ValueTask<List<MemeRepoItemViewModel>> LoadFolderMemesByFolderId(Guid folderId)
        {
            var targetFolder = await _dal.For<Folder>().Get.LoadWith(x => x.Memes).FirstAsync(x => x.Id == folderId);

            var memes = _mapper.Map<List<MemeRepoItemViewModel>>(targetFolder.Memes);

            return memes;
        }

        public async ValueTask<List<TagViewModel>> LoadFolderTagsByFolderId(Guid folderId)
        {
            var targetFolder = await _dal.For<Folder>().Get.LoadWith(x => x.FolderTags).FirstAsync(x => x.Id == folderId);

            var tagIds = targetFolder.FolderTags.Select(x => x.TagId);
            var tags = _appDataState.Value.Tags.Where(x => tagIds.Any(t => t == x.Id)).ToList();

            return tags;
        }

        public async ValueTask<List<TagViewModel>> LoadMemeTagsByMemeId(Guid folderId)
        {
            var targetFolder = await _dal.For<Meme>().Get.LoadWith(x => x.MemeTags).FirstAsync(x => x.Id == folderId);

            var tagIds = targetFolder.MemeTags.Select(x => x.TagId);
            var tags = _appDataState.Value.Tags.Where(x => tagIds.Any(t => t == x.Id)).ToList();

            return tags;
        }

        #endregion
    }
}
