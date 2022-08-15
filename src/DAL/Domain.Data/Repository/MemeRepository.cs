using DALQueryChain.Linq2Db.Repositories;

namespace Domain.Data.Repository
{
    internal class MemeRepository : BaseRepository<MemeRepoDbContext, Meme>
    {
        #region Ctors

        public MemeRepository(MemeRepoDbContext context) : base(context)
        {
        }

        #endregion

        protected override void OnBeforeInsert(Meme model)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();

            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;
        }

        protected override void OnBeforeUpdate(Meme model)
        {
            model.UpdatedAt = DateTime.UtcNow;
        }

        protected override Task OnBeforeInsertAsync(Meme model)
        {
            OnBeforeInsert(model);
            return Task.CompletedTask;
        }

        protected override Task OnBeforeUpdateAsync(Meme model)
        {
            OnBeforeUpdate(model);
            return Task.CompletedTask;
        }
    }
}
