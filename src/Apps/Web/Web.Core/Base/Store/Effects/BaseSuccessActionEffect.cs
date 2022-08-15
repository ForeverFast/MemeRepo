using Web.Core.Base.Store.Actions;

namespace Web.Core.Base.Store.Effects
{
    internal abstract class BaseSuccessActionEffect<T> : BaseActionResultEffect<T> where T : BaseSuccessAction
    {
        #region Ctors

        public BaseSuccessActionEffect(ISnackbar snackbar) : base(snackbar)
        {

        }

        #endregion

        public override Task HandleAsync(T action, IDispatcher dispatcher)
        {
            if (!string.IsNullOrEmpty(action.SuccessMessage))
            {
                _snackbar.Add(action.SuccessMessage, Severity.Success);
            }

            return Task.CompletedTask;
        }
    }
}
