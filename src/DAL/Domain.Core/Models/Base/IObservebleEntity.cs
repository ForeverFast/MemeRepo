namespace Domain.Core.Models.Base
{
    public interface IObservebleEntity : ICreatedTrackEntity
    {
        DateTimeOffset UpdatedAt { get; set; }
    }
}
