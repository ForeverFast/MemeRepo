using MudBlazor.Utilities;
using Web.Core.Enums.Components;
using Web.Core.Models.Components;
using Web.Core.Store.AppData.Actions.FolderActions.DeleteFolderActions;
using Web.Core.Store.AppData.Actions.FolderActions.UpdateFolderActions;
using Web.Core.Store.AppData.Actions.MemeActions.DeleteMemeActions;
using Web.Core.Store.AppData.Actions.MemeActions.UpdateMemeActions;

namespace Web.Core.Components.ItemsContainer
{
    public partial class MemeRepoContainerItem
    {
        #region Params

        [Parameter] public MemeRepoItemViewModel Data { get; set; } = new MemeRepoItemViewModel();
        [Parameter] public string? Width { get; set; }
        [Parameter] public string? Height { get; set; }

        #endregion

        #region UI Fields

        #endregion

        #region Component Css/Style

        protected virtual string Classname => new CssBuilder("memerepo-container-item")
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
