using Web.Core.Base.Store.Actions;

namespace Web.Core.Store.AppData.Actions.MemeActions.DeleteMemeActions
{
    internal record DeleteMemeSuccessAction : BaseSuccessAction
    {
        public DeleteMemeSuccessAction(Guid deletedFolder)
        {
            DeletedFolder = deletedFolder;
        }

        public Guid DeletedFolder { get; }
    }
}
