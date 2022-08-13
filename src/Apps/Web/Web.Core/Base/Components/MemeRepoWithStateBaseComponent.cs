using Web.Core.Enums.Components.StateContainer;

namespace Web.Core.Base.Components
{
    public class MemeRepoWithStateBaseComponent : MemeRepoBaseComponent
    {
        #region UI Props/Fields

        protected virtual ComponentState State { get; set; } = ComponentState.Loading;

        #endregion
    }
}
