
using Web.Core.Models;

namespace Web.Core.Store.AppData.Actions.TagActions.CreateTagActions
{
    internal record CreateMemeSuccessAction : BaseSuccessAction
    {
        public CreateMemeSuccessAction(MemeViewModel newMeme)
        {
            NewMeme = newMeme;
        }

        public MemeViewModel NewMeme { get; }
    }
}
