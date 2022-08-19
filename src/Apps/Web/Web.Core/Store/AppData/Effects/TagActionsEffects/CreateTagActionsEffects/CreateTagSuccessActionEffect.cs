using Web.Core.Store.AppData.Actions.TagActions.CreateTagActions;

namespace Web.Core.Store.AppData.Effects.TagActionsEffects.CreateTagActionsEffects
{
    internal class CreateTagSuccessActionEffect : BaseSuccessActionEffect<CreateTagSuccessAction>
    {
        public CreateTagSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
