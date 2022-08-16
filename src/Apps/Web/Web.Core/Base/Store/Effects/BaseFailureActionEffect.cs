﻿using Web.Core.Base.Store.Actions;

namespace Web.Core.Base.Store.Effects
{
    internal abstract class BaseFailureActionEffect<T> : BaseActionResultEffect<T> where T : BaseFailureAction
    {
        #region Ctors

        public BaseFailureActionEffect(ISnackbar snackbar) : base(snackbar)
        {
        }

        #endregion

        public override Task HandleAsync(T action, IDispatcher dispatcher)
        {
            if (action.ShowMessage)
            {
                _snackbar.Add(action.ErrorMessage, Severity.Error);
            }

            return Task.CompletedTask;
        }
    }
}
