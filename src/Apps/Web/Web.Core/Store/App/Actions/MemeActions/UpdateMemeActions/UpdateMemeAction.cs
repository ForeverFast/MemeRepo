namespace Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions
{
    internal record UpdateMemeAction
    {
        public UpdateMemeAction(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; init; }
    }
}
