namespace Web.Core.Store.App.Actions.TagActions.DeleteTagActions
{
    internal record DeleteTagSuccessAction : BaseSuccessAction
    {
        public DeleteTagSuccessAction(Guid deletedTagId)
        {
            DeletedTagId = deletedTagId;
        }

        public Guid DeletedTagId { get; }
    }
}
