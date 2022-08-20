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

            var memeQC = GetQueryChain<Meme>();
            var folderQC = GetQueryChain<Folder>();
            var folderTagsQC = GetQueryChain<FolderTag>();

            var fullModel = await folderQC.Get
                .LoadWith(x => x.Memes)
                .LoadWith(x => x.Folders)
                .LoadWith(x => x.FolderTags)
                .FirstAsync(x => x.Id == model.Id);

            await memeQC.Delete.BulkDeleteAsync(fullModel.Memes);
            await folderQC.Delete.BulkDeleteAsync(fullModel.Folders);
            await folderTagsQC.Delete.BulkDeleteAsync(fullModel.FolderTags);
        }

        protected override async Task OnBeforeBulkDeleteAsync(IEnumerable<Folder> models)
        {
            OnBeforeBulkDelete(models);
            foreach (var model in models)
            {
                var memeQC = GetQueryChain<Meme>();
                var folderQC = GetQueryChain<Folder>();
                var folderTagsQC = GetQueryChain<FolderTag>();

                var fullModel = await folderQC.Get
                    .LoadWith(x => x.Memes)
                    .LoadWith(x => x.Folders)
                    .LoadWith(x => x.FolderTags)
                    .FirstAsync(x => x.Id == model.Id);

                await memeQC.Delete.BulkDeleteAsync(fullModel.Memes);
                await folderQC.Delete.BulkDeleteAsync(fullModel.Folders);
                await folderTagsQC.Delete.BulkDeleteAsync(fullModel.FolderTags);
            }
        }
    }
}
