using DALQueryChain.Linq2Db.Repositories;

namespace Domain.Data.Repository
{
    internal class FolderRepository : BaseRepository<MemeRepoDbContext, Folder>
    {
        #region Ctors

        public FolderRepository(MemeRepoDbContext context) : base(context)
        {

        }

        #endregion

        protected override void OnBeforeInsert(Folder model)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();

            model.CreatedAt = DateTime.UtcNow;
            model.UpdatedAt = DateTime.UtcNow;
        }

        protected override void OnBeforeUpdate(Folder model)
        {
            model.UpdatedAt = DateTime.UtcNow;
        }

        protected override Task OnBeforeInsertAsync(Folder model)
        {
            OnBeforeInsert(model);
            return Task.CompletedTask;
        }

        protected override Task OnBeforeUpdateAsync(Folder model)
        {
            OnBeforeUpdate(model);
            return Task.CompletedTask;
        }

        protected override async Task OnBeforeDeleteAsync(Folder model)
        {
            OnBeforeDelete(model);
            var fullModel = await GetQueryChain<Folder>().Get
                .LoadWith(x => x.Memes)
                .LoadWith(x => x.Folders)
                .FirstAsync(x => x.Id == model.Id);

            await GetQueryChain<Meme>().Delete.BulkDeleteAsync(fullModel.Memes);
            await GetQueryChain<Folder>().Delete.BulkDeleteAsync(fullModel.Folders);
        }

        protected override async Task OnBeforeBulkDeleteAsync(IEnumerable<Folder> models)
        {
            OnBeforeBulkDelete(models);
            foreach (var model in models)
            {
                var folderQC = GetQueryChain<Folder>();
                var memeQC = GetQueryChain<Meme>();

                var fullModel = await folderQC.Get
                    .LoadWith(x => x.Memes)
                    .LoadWith(x => x.Folders)
                    .FirstAsync(x => x.Id == model.Id);

                await memeQC.Delete.BulkDeleteAsync(fullModel.Memes);
                await folderQC.Delete.BulkDeleteAsync(fullModel.Folders);
            }
        }
    }
}
