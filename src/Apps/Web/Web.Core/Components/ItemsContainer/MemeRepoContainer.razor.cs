using Web.Core.Models.Components;

namespace Web.Core.Components.ItemsContainer
{
    public partial class MemeRepoContainer
    {
        #region Params

        [Parameter] public List<MemeRepoItemViewModel> Items { get; set; } = new();
        [Parameter] public RenderFragment? EmptyContent { get; set; }

        #endregion
    }
}
