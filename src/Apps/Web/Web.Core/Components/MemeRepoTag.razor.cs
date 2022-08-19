using MudBlazor.Utilities;
using Web.Core.Models;

namespace Web.Core.Components
{
    public partial class MemeRepoTag : MemeRepoBaseComponent
    {
        #region Params

        [Parameter] public TagViewModel Data { get; set; } = new();

        #endregion

        #region Component Css/Style

        protected virtual string Classname => new CssBuilder("memerepo-container-item")
           .AddClass(Class)
           .Build();

        protected virtual string Stylename => new StyleBuilder()
            .AddStyle(Style)
            .Build();

        #endregion
    }
}
