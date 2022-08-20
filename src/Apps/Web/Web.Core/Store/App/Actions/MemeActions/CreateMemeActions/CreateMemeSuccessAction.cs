using Web.Core.Models;

namespace Web.Core.Store.App.Actions.MemeActions.CreateMemeActions
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
