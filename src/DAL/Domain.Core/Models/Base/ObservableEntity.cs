namespace Domain.Core.Models.Base
{
    public abstract class ObservableEntity : CreatedTrackEntity, IObservebleEntity
    {
        public DateTimeOffset UpdatedAt { get; set; } 
    }
}
