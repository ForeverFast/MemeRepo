using Web.Core.Models.Components;

namespace Web.Core.Store.App.Actions.DataActions.SetCurrentContentActions
{
    internal record SetCurrentContentSuccessAction : BaseSuccessAction
    {
        public SetCurrentContentSuccessAction(List<MemeRepoItemViewModel> items,
                                              ContentType currentContentType,
                                              Guid? currentContentId)
        {
            Items = items;
            CurrentContentType = currentContentType;
            CurrentContentId = currentContentId;
        }

        public ContentType CurrentContentType { get; }
        public Guid? CurrentContentId { get; }
        public List<MemeRepoItemViewModel> Items { get; }
    }
}
