using Microsoft.AspNetCore.Components.Web;
using Web.Core.Models.Components.Dialogs;

namespace Web.Core.Components.DialogComponents.Shared
{
    public partial class PathsPack
    {
        #region Params

        [Parameter] public PathsPackViewModel Data { get; set; } = new();
        [Parameter] public EventCallback<PathsPackViewModel> OnDeleteClick { get; set; }

        #endregion
    }
}
