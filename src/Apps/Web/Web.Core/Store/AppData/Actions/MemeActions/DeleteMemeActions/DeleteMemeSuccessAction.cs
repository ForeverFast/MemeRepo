namespace Web.Core.Store.AppData.Actions.MemeActions.DeleteMemeActions
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
