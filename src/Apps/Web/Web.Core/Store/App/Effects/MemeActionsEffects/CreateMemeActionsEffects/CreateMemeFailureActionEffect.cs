using Web.Core.Store.App.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Store.App.Effects.MemeActionsEffects.CreateMemeActionsEffects
{
    internal class CreateMemeFailureActionEffect : BaseFailureActionEffect<CreateMemeFailureAction>
    {
        public CreateMemeFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
