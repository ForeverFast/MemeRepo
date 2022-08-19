using Web.Core.Store.AppData.Actions.TagActions.DeleteTagActions;

namespace Web.Core.Store.AppData.Effects.TagActionsEffects.DeleteTagActionsEffects
{
    internal class DeleteTagSuccessActionEffect : BaseSuccessActionEffect<DeleteTagSuccessAction>
    {
        public DeleteTagSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }
    }
}
