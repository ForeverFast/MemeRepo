using MudBlazor.Utilities;
using Web.Core.Models.Components;

namespace Web.Core.Components.ItemsContainer
{
    public partial class MemeRepoContainerItem
    {
        #region Params

        [Parameter] public MemeRepoItemViewModel Data { get; set; } = new MemeRepoItemViewModel();
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public string? ContextMenuType { get; set; }

        #endregion

        #region UI Fields

        private string contextMenuType => ContextMenuType ?? ContextMemuAlias.None;
           
        #endregion

        #region Component Css/Style

        protected virtual string Classname => new CssBuilder()
            .AddClass(Class)
            .Build();

        protected virtual string Stylename => new StyleBuilder()
            .AddStyle(Style)
            .Build();

        #endregion
    }
}

/*
   var abslPath = Path.Combine("wwwroot", state.GetFileRelativePath(x.ParentFolderId!.Value, x.Path));
   x.Path = "data:image/png;base64, " + Convert.ToBase64String(File.ReadAllBytes(abslPath));
 */
