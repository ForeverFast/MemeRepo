using Web.Core.Base.Store.States;
using Web.Core.Models.Components;

namespace Web.Core.Store.ContentData
{
    internal record ContentDataState : BaseState
    {
        public List<MemeRepoItemViewModel> Items { get; init; } = new();
    }
}
