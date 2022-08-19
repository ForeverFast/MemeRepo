namespace Web.Core.Base.Components
{
    public class MemeRepoBaseFluxorLayout : FluxorLayout
    {
        protected void BaseUpdateUIAction(object action) => InvokeAsync(() => this.StateHasChanged());
    }
}
