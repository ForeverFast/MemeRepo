using DALQueryChain.Interfaces;
using Web.Core.Base.Store.Effects;
using Web.Core.Models;
using Web.Core.Models.Components;
using Web.Core.Store.AppData.Actions.LoadAppDataActions;

namespace Web.Core.Store.AppData.Effects
{
    internal class LoadAppDataEffect : BaseDataEffect<LoadAppDataAction>
    {
        #region Ctors

        public LoadAppDataEffect(IDALQueryChain<MemeRepoDbContext> dal, IMapper mapper) : base(dal, mapper)
        {
        }

        #endregion

        public override async Task HandleAsync(LoadAppDataAction action, IDispatcher dispatcher)
        {
            try
            {
                var folders = await _dal.For<Folder>().Get.LoadWith(x => x.FolderTags).ToListAsync();
                var tags = await _dal.For<Tag>().Get.ToListAsync();

                var folderVMs = _mapper.Map<HashSet<FolderTreeViewModel>>(folders).ToTree();
                var tagVMs = _mapper.Map<List<TagViewModel>>(tags);

                dispatcher.Dispatch(new LoadAppDataSuccessAction
                {
                    Folders = folderVMs,
                    Tags = tagVMs,
                });
            }
            catch (Exception)
            {
                dispatcher.Dispatch(new LoadAppDataFailureAction
                {
                    ErrorMessage = "Ошибка во время загрузки.",
                });
            }
        }
    }
}
