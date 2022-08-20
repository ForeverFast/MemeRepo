namespace Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions
{
    internal record DeleteMemeSuccessAction : BaseSuccessAction
    {
        public DeleteMemeSuccessAction(Guid deletedMemeId)
        {
            DeletedMemeId = deletedMemeId;
        }

        public Guid DeletedMemeId { get; }
    }
}
