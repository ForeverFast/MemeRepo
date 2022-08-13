namespace Web.Core.Base.Components
{
    public class MemeRepoContainerWithStateBaseComponent : MemeRepoWithStateBaseComponent
    {
        #region Params

        [Parameter] public RenderFragment? ChildContent { get; set; }

        #endregion
    }
}
