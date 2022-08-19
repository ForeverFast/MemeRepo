namespace Web.Core.Store.AppData.Actions.TagActions.UpdateTagActions
{
    internal record UpdateTagAction
    {
        public UpdateTagAction(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
