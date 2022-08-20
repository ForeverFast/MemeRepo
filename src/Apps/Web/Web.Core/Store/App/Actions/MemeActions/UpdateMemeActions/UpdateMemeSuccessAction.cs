using Web.Core.Models;

namespace Web.Core.Store.App.Actions.MemeActions.UpdateMemeActions
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
