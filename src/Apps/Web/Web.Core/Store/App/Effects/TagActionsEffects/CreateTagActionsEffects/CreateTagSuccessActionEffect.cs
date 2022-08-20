using Web.Core.Store.App.Actions.TagActions.CreateTagActions;

namespace Web.Core.Store.App.Effects.TagActionsEffects.CreateTagActionsEffects
{
    internal class CreateTagSuccessActionEffect : BaseSuccessActionEffect<CreateTagSuccessAction>
    {
        public CreateTagSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
