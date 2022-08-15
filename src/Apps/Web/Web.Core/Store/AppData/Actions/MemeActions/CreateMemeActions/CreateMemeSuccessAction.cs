using Web.Core.Base.Store.Actions;
using Web.Core.Models;

namespace Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions
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
