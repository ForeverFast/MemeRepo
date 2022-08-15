using Web.Core.Base.Store.Actions;
using Web.Core.Models;

namespace Web.Core.Store.AppData.Actions.MemeActions.UpdateMemeActions
{
    internal record UpdateMemeSuccessAction : BaseSuccessAction
    {
        public UpdateMemeSuccessAction(MemeViewModel newMeme)
        {
            NewMeme = newMeme;
        }

        public MemeViewModel NewMeme { get; }
    }
}
