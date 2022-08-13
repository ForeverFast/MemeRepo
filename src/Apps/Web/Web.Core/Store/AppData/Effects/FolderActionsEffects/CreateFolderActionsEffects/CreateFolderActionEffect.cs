using DALQueryChain.Interfaces;
using Web.Core.Base.Store.Effects;
using Web.Core.Models.Components;
using Web.Core.Store.AppData.Actions.FolderActions.CreateFolderActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.CreateFolderActionsEffects
{
    internal class CreateFolderActionEffect : BaseDataEffect<CreateFolderAction>
    {
        #region Ctors

        public CreateFolderActionEffect(IDALQueryChain<MemeRepoClientContext> dal, IMapper mapper) : base(dal, mapper)
        {
        }

        #endregion

        public override async Task HandleAsync(CreateFolderAction action, IDispatcher dispatcher)
        {
            try
            {
                var folder = _mapper.Map<Folder>(action);
                var createdFolder = await _dal.For<Folder>().Insert.InsertWithObjectAsync(folder);
                var result = _mapper.Map<FolderTreeViewModel>(createdFolder);

                if (result == null)
                    throw new Exception();

                dispatcher.Dispatch(new CreateFolderSuccessAction(result));
            }
            catch (Exception)
            {
                dispatcher.Dispatch(new CreateFolderFailureAction
                {
                    FailureMessage = "",
                    ErrorMessage = "Failed to create Folder"
                });
            }
        }
    }
}
