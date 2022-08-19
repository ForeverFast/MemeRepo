namespace Web.Core.Store.AppData.Actions.TagActions.DeleteTagActions
{
    internal record DeleteTagAction
    {
        public DeleteTagAction(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
