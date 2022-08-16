using Web.Core.Enums;
using Web.Core.Models.Components.Search;

namespace Web.Core.Store.AppData.Actions.ChangeStateActions
{
    internal record SetCurrentContentAction
    {
        public SetCurrentContentAction(ContentType currentContentType)
        {
            CurrentContentType = currentContentType;
        }

        public ContentType CurrentContentType { get; }
        public Guid? CurrentContentId { get; init; }
        public FilterModel? Filter { get; init; }
    }
}
