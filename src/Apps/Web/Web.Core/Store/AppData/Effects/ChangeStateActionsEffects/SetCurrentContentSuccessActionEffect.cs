using Web.Core.Base.Store.Actions;
using Web.Core.Models.Components;

namespace Web.Core.Store.AppData.Effects.ChangeStateActionsEffects
{
    internal record SetCurrentContentSuccessActionEffect : BaseSuccessAction
    {
        public SetCurrentContentSuccessActionEffect(List<MemeRepoItemViewModel> items)
        {
            Items = items;
        }

        public List<MemeRepoItemViewModel> Items { get; }
    }
}
