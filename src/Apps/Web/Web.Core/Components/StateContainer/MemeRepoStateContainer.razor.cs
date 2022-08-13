using Web.Core.Enums.Components.StateContainer;

namespace Web.Core.Components.StateContainer
{
    public partial class MemeRepoStateContainer
    {
        #region Params

        [Parameter] public ComponentState State { get; set; } = ComponentState.Loading;
        [Parameter] public RenderFragment? LoadingFragment { get; set; }
        [Parameter] public RenderFragment? ContentFragment { get; set; }
        [Parameter] public RenderFragment? ErrorFragment { get; set; }

        #endregion

        protected virtual RenderFragment? GetComponentStateFragment()
        {
            return State switch
            {
                ComponentState.Loading => LoadingFragment,
                ComponentState.Content => ContentFragment,
                ComponentState.Error => ErrorFragment,
                _ => ErrorFragment
            };
        }
    }
}
