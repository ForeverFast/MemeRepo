using Web.Core.Store.App.Actions.TagActions.CreateTagActions;

namespace Web.Core.Store.App.Effects.TagActionsEffects.CreateTagActionsEffects
{
    internal class CreateTagFailureActionEffect : BaseFailureActionEffect<CreateTagFailureAction>
    {
        public CreateTagFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
