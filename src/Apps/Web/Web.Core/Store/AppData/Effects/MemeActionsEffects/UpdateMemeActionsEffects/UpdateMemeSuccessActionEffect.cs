using Web.Core.Base.Store.Effects;
using Web.Core.Store.AppData.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Store.AppData.Effects.MemeActionsEffects.UpdateMemeActionsEffects
{
    internal class UpdateMemeSuccessActionEffect : BaseSuccessActionEffect<UpdateMemeSuccessAction>
    {
        public UpdateMemeSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
