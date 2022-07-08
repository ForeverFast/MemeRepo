namespace Domain.Core.Models.Base
{
    public abstract class DomainEntity : IDomainEntity
    {
        public Guid Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
