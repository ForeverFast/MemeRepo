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
            if (action.ShowMessage)
            {
                _snackbar.Add(action.SuccessMessage, Severity.Success);
            }

            return Task.CompletedTask;
        }
    }
}
