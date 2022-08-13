using System.ComponentModel.DataAnnotations;

namespace Web.Core.Models.Components
{
    public class FolderDialogViewModel
    {
        public Guid Id { get; set; }
        public Guid? ParentFolderId { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<TagViewModel> Tags { get; set; } = new();
    }
}
