using MudBlazor.Utilities;
using Web.Core.Models;

namespace Web.Core.Components
{
    public partial class MemeRepoTag : MemeRepoBaseComponent
    {
        #region Params

        [Parameter] public TagViewModel Data { get; set; } = new();
        [Parameter] public EventCallback<TagViewModel> OnClickAction { get; set; }

        #endregion

        #region Component Css/Style

        protected virtual string Classname => new CssBuilder("memerepo-container-item")
           .AddClass(Class)
           .Build();

        protected virtual string Stylename => new StyleBuilder()
            .AddStyle(Style)
            .Build();

        #endregion

        #region Internal events

        private async Task OnClick() => await OnClickAction.InvokeAsync(Data);

        #endregion
    }
}
