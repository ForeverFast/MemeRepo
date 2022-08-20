namespace Web.Core.Store.App.Actions.MemeActions.DeleteMemeActions
{
    internal record DeleteMemeAction
    {
        public DeleteMemeAction(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
