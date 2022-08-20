using Web.Core.Store.App.Actions.MemeActions.CreateMemeActions;

namespace Web.Core.Store.App.Effects.MemeActionsEffects.CreateMemeActionsEffects
{
    internal class CreateMemeSuccessActionEffect : BaseSuccessActionEffect<CreateMemeSuccessAction>
    {
        public CreateMemeSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
