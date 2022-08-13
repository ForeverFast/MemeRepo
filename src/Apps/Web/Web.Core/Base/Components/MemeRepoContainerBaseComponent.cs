namespace Web.Core.Base.Components
{
    public abstract class MemeRepoContainerBaseComponent : MemeRepoBaseComponent
    {
        #region Params

        [Parameter] public RenderFragment? ChildContent { get; set; }

        #endregion
    }
}
