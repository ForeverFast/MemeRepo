using MudBlazor.Utilities;
using Web.Core.Models.Components;

namespace Web.Core.Components
{
    public partial class MemeRepoItem
    {
        #region Params

        [Parameter] public MemeRepoItemViewModel Data { get; set; } = new MemeRepoItemViewModel(); 
        [Parameter] public string? Width { get; set; }
        [Parameter] public string? Height { get; set; }

        #endregion

        #region Component Css/Style

        protected virtual string Classname => new CssBuilder("memerepo-item")
           .AddClass(Class)
           .Build();

        protected virtual string Stylename => new StyleBuilder()
            .AddStyle("width", $"{Width}", !string.IsNullOrEmpty(Width))
            .AddStyle("height", $"{Height}", !string.IsNullOrEmpty(Height))
            .AddStyle(Style)
            .Build();

        #endregion
    }
}
