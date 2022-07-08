namespace Domain.Core.Models
{
    public class Tag : CreatedTrackEntity
    {
        public string Title { get; set; }

        public override string ToString() => this.Title;
    }
}
