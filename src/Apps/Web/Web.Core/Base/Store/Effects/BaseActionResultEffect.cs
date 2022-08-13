
namespace Web.Core.Base.Store.Effects
{
    internal abstract class BaseActionResultEffect<T> : Effect<T> where T : class
    {
        #region Injects

        protected readonly ISnackbar _snackbar;

        #endregion

        #region Ctors

        protected BaseActionResultEffect(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        #endregion
    }
}
