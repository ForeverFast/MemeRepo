using System.ComponentModel.DataAnnotations;

namespace Web.Core.Models.Components.Dialogs
{
    public class TagDialogViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; } = string.Empty;
    }
}
