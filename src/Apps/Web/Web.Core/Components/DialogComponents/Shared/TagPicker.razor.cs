using Web.Core.Models.Components.Dialogs;
using Web.Core.Store.App;

namespace Web.Core.Components.DialogComponents.Shared
{
    public partial class TagPicker
    {
        #region Params

        [Parameter] public List<Guid> CurrentItemTags { get; set; } = new();
        [Parameter] public List<ItemTagViewModel> SelectedTags { get; set; } = new();
        [Parameter] public EventCallback<List<ItemTagViewModel>> SelectedTagsChanged { get; set; }

        #endregion

        #region Injects

        [Inject] IState<AppState>? _appState { get; init; }

        [Inject] IMapper? _mapper { get; init; }

        #endregion

        #region UI Fields

        private string? filterTitle = string.Empty;

        private List<ItemTagViewModel> allTags = new();
        private List<ItemTagViewModel> otherTags = new();
        private List<ItemTagViewModel> otherTagsFiltered
           => !string.IsNullOrEmpty(filterTitle)
           ? otherTags.Where(x => x.Title.ToLower().Contains(filterTitle.ToLower())).ToList()
           : otherTags;
        
        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            allTags.AddRange(_mapper!.Map<List<ItemTagViewModel>>(_appState!.Value.Tags));

            SelectedTags.AddRange(allTags.Where(x => CurrentItemTags.Contains(x.Id)));
            SelectedTags.ForEach(x => x.IsSelected = true);
            otherTags.AddRange(allTags.Except(SelectedTags));

            base.OnInitialized();
        }

        #endregion

        #region Internal events

        private void OnItemTagClick(ItemTagViewModel tag)
        {
            if (tag.IsSelected)
            {
                tag.IsSelected = false;
                otherTags.Add(tag);
                SelectedTags.Remove(tag);
            }
            else
            {
                tag.IsSelected = true;
                otherTags.Remove(tag);
                SelectedTags.Add(tag);
            }
        }

        #endregion
    }
}
