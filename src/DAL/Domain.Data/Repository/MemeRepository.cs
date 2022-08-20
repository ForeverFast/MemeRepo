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

        protected override async Task OnBeforeDeleteAsync(Meme model)
        {
            OnBeforeDelete(model);

            var memeQC = GetQueryChain<Meme>();
            var memeTagsQC = GetQueryChain<MemeTag>();

            var fullModel = await memeQC.Get
                .LoadWith(x => x.MemeTags)
                .FirstAsync(x => x.Id == model.Id);

            await memeTagsQC.Delete.BulkDeleteAsync(fullModel.MemeTags);
        }

        protected override async Task OnBeforeBulkDeleteAsync(IEnumerable<Meme> models)
        {
            OnBeforeBulkDelete(models);
            foreach (var model in models)
            {
                var memeQC = GetQueryChain<Meme>();
                var memeTagsQC = GetQueryChain<MemeTag>();

                var fullModel = await memeQC.Get
                    .LoadWith(x => x.MemeTags)
                    .FirstAsync(x => x.Id == model.Id);

                await memeTagsQC.Delete.BulkDeleteAsync(fullModel.MemeTags);
            }
        }
    }
}
