using Web.Core.Models;

namespace Web.Core.Store.App.Actions.TagActions.UpdateTagActions
{
    internal record UpdateTagSuccessAction : BaseSuccessAction
    {
        public UpdateTagSuccessAction(TagViewModel updatedTag)
        {
            UpdatedTag = updatedTag;
        }

        public TagViewModel UpdatedTag { get; }
    }
}
