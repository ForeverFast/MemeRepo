using Web.Core.Base.Store.Actions;
using Web.Core.Models;

namespace Web.Core.Store.AppData.Actions.MemeActions.UpdateMemeActions
{
    internal record UpdateMemeSuccessAction : BaseSuccessAction
    {
        public UpdateMemeSuccessAction(MemeViewModel updatedMeme)
        {
            UpdatedMeme = updatedMeme;
        }

        public MemeViewModel UpdatedMeme { get; }
    }
}
