using Web.Core.Store.AppData.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Store.AppData.Effects.MemeActionsEffects.CreateMemeActionsEffects
{
    internal class CreateMemeFailureActionEffect : BaseFailureActionEffect<CreateMemeFailureAction>
    {
        public CreateMemeFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
