namespace Web.Core.Base.Components
{
    public abstract class MemeRepoBaseFluxorComponent : FluxorComponent
    {
        protected void BaseUpdateUIAction(object action) => InvokeAsync(() => this.StateHasChanged());
    }
}
