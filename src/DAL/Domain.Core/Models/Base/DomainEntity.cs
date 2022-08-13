using DALQueryChain.Interfaces;

namespace Domain.Core.Models.Base
{
    public abstract class DomainEntity : IDomainEntity, IDbModelBase
    {
        public Guid Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
