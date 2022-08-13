namespace Domain.Core.Models
{
    [Table("Tags")]
    public class Tag : ObservableEntity
    {
        public string Title { get; set; } = string.Empty;

        public override string ToString() => this.Title;
    }
}
