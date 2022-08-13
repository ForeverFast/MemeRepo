namespace Domain.Core.Models.Base
{
    public abstract class CreatedTrackEntity : DomainEntity, ICreatedTrackEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
    }
}
