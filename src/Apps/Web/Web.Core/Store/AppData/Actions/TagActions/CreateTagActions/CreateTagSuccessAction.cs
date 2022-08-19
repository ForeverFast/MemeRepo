using Web.Core.Models;

namespace Web.Core.Store.AppData.Actions.TagActions.CreateTagActions
{
    internal record CreateTagSuccessAction : BaseSuccessAction
    {
        public CreateTagSuccessAction(TagViewModel newTag)
        {
            NewTag = newTag;
        }

        public TagViewModel NewTag { get; }
    }
}
