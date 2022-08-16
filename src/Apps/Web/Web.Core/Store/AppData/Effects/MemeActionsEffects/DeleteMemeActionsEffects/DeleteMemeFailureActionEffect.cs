using Web.Core.Base.Store.Effects;
using Web.Core.Store.AppData.Actions.MemeActions.DeleteMemeActions;

namespace Web.Core.Store.AppData.Effects.FolderActionsEffects.DeleteMemeActionsEffects
{
    internal class DeleteMemeFailureActionEffect : BaseFailureActionEffect<DeleteMemeFailureAction>
    {
        public DeleteMemeFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
