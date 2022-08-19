using DALQueryChain.Interfaces;
using Web.Core.Models.Components;
using Web.Core.Store.AppData.Actions.ChangeStateActions.SetCurrentContentActions;

namespace Web.Core.Store.AppData.Effects.ChangeStateActionsEffects.SetCurrentContentActionsEffect.cs
{
    internal class SetCurrentContentActionEffect : BaseDataEffect<SetCurrentContentAction>
    {
        #region Injects

        protected readonly IState<AppDataState> _appDataState;

        #endregion

        #region Ctors

        public SetCurrentContentActionEffect(IDALQueryChain<MemeRepoDbContext> dal, IMapper mapper, IState<AppDataState> appDataState) : base(dal, mapper)
        {
            _appDataState = appDataState;
        }

        #endregion

        public override Task HandleAsync(SetCurrentContentAction action, IDispatcher dispatcher)
        {
            try
            {
                return action.CurrentContentType switch
                {
                    ContentType.FolderPage => LoadFolderPage(action, dispatcher),
                    ContentType.TagPage => LoadTagPage(action, dispatcher),
                    ContentType.SearchPage => LoadSearchPage(action, dispatcher),
                    ContentType.Other => LoadOtherPage(action, dispatcher),
                    _ => LoadOtherPage(action, dispatcher),
                };
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new SetCurrentContentFailureAction
                {
                    ErrorMessage = $"Ошибка загрузки данных. Error: {ex.Message}",
                    Exception = ex,
                });
                return Task.CompletedTask;
            }
        }

        private Task LoadOtherPage(SetCurrentContentAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new SetCurrentContentSuccessAction(new(), action.CurrentContentType, action.CurrentContentId));
            return Task.CompletedTask;
        }

        private async Task LoadFolderPage(SetCurrentContentAction action, IDispatcher dispatcher)
        {
            var targetFolder = await _dal.For<Folder>().Get
                .LoadWith(x => x.Memes)
                .FirstOrDefaultAsync(x => x.Id == action.CurrentContentId);
            if (targetFolder == null)
                throw new Exception("Папка не найдена.");

            var memes = _mapper.Map<List<MemeRepoItemViewModel>>(targetFolder.Memes);

            dispatcher.Dispatch(new SetCurrentContentSuccessAction(memes, action.CurrentContentType, action.CurrentContentId));
        }

        private async Task LoadTagPage(SetCurrentContentAction action, IDispatcher dispatcher)
        {
            var targetTag = await _dal.For<Tag>().Get
               .LoadWith(x => x.MemeTags)
               .LoadWith(x => x.FolderTags)
               .FirstOrDefaultAsync(x => x.Id == action.CurrentContentId);
            if (targetTag == null)
                throw new Exception("Папка не найдена.");

            var folders = _mapper.Map<List<MemeRepoItemViewModel>>(targetTag.FolderTags.Select(x => x.Folder));
            var memes = _mapper.Map<List<MemeRepoItemViewModel>>(targetTag.MemeTags.Select(x => x.Meme));

            var result = folders.Concat(memes).ToList();

            dispatcher.Dispatch(new SetCurrentContentSuccessAction(result, action.CurrentContentType, action.CurrentContentId));
        }

        private async Task LoadSearchPage(SetCurrentContentAction action, IDispatcher dispatcher)
        {
            var folderQuery = _dal.For<Folder>().Get
                .LoadWith(x => x.FolderTags);
            var memeQuery = _dal.For<Meme>().Get
                .LoadWith(x => x.MemeTags);

            if (action.Filter != null)
            {
                var filter = action.Filter;
                if (!string.IsNullOrEmpty(filter.SearchTitle))
                {
                    folderQuery.Where(x => x.Title.Contains(filter.SearchTitle));
                    memeQuery.Where(x => x.Title.Contains(filter.SearchTitle));
                }
                if (filter.Tags.Count > 0)
                {
                    folderQuery.Where(x => x.FolderTags.All(ft => filter.Tags.Contains(ft.TagId)));
                    memeQuery.Where(x => x.MemeTags.All(mt => filter.Tags.Contains(mt.TagId)));
                }

                var folderResult = await folderQuery.ToListAsync();
                var memeResult = await memeQuery.ToListAsync();

                var folders = _mapper.Map<List<MemeRepoItemViewModel>>(folderResult);
                var memes = _mapper.Map<List<MemeRepoItemViewModel>>(memeResult);

                var result = folders.Concat(memes).ToList();

                dispatcher.Dispatch(new SetCurrentContentSuccessAction(result, action.CurrentContentType, action.CurrentContentId));
            }

            throw new Exception("Укажите фильтры");
        }
    }
}
