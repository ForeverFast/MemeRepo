using Web.Core.Models.Components.Dialogs;

namespace Web.Core.Components.DialogComponents.Shared
{
    public partial class ItemTag : MemeRepoBaseComponent
    {
        #region Params

        [Parameter] public ItemTagViewModel Data { get; set; } = new();
        [Parameter] public EventCallback<ItemTagViewModel> OnActionButtonClick { get; set; }

        #endregion

        #region UI Fields

        private string iconType => Data.IsSelected ? Icons.Outlined.Close : Icons.Outlined.Add;

        #endregion
    }
}
