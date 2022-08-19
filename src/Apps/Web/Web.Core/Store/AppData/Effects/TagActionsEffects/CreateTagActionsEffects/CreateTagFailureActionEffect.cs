using Web.Core.Store.AppData.Actions.TagActions.CreateTagActions;

namespace Web.Core.Store.AppData.Effects.TagActionsEffects.CreateTagActionsEffects
{
    internal class CreateTagFailureActionEffect : BaseFailureActionEffect<CreateTagFailureAction>
    {
        public CreateTagFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
