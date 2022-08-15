using Web.Core.Base.Store.Effects;
using Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Store.AppData.Effects.MemeActionsEffects.CreateMemeActionsEffects
{
    internal class CreateMemeSuccessActionEffect : BaseSuccessActionEffect<CreateMemeSuccessAction>
    {
        public CreateMemeSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
