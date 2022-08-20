using DALQueryChain.Linq2Db.Repositories;

namespace Domain.Data.Repository
{
    internal class TagRepository : BaseRepository<MemeRepoDbContext, Tag>
    {
        #region Ctors

        public TagRepository(MemeRepoDbContext context) : base(context)
        {
        }

        #endregion

        protected override void OnBeforeInsert(Tag model)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();

            model.CreatedAt = DateTime.UtcNow;
        }

        protected override Task OnBeforeInsertAsync(Tag model)
        {
            OnBeforeInsert(model);
            return Task.CompletedTask;
        }

        protected override async Task OnBeforeDeleteAsync(Tag model)
        {
            OnBeforeDelete(model);

            var tagQC = GetQueryChain<Tag>();
            var memeTagsQC = GetQueryChain<MemeTag>();
            var folderTagsQC = GetQueryChain<FolderTag>();

            var fullModel = await tagQC.Get
                .LoadWith(x => x.MemeTags)
                .LoadWith(x => x.FolderTags)
                .FirstAsync(x => x.Id == model.Id);

            await memeTagsQC.Delete.BulkDeleteAsync(fullModel.MemeTags);
            await folderTagsQC.Delete.BulkDeleteAsync(fullModel.FolderTags);
        }

        protected override async Task OnBeforeBulkDeleteAsync(IEnumerable<Tag> models)
        {
            OnBeforeBulkDelete(models);
            foreach (var model in models)
            {
                var tagQC = GetQueryChain<Tag>();
                var memeTagsQC = GetQueryChain<MemeTag>();
                var folderTagsQC = GetQueryChain<FolderTag>();

                var fullModel = await tagQC.Get
                    .LoadWith(x => x.MemeTags)
                    .LoadWith(x => x.FolderTags)
                    .FirstAsync(x => x.Id == model.Id);

                await memeTagsQC.Delete.BulkDeleteAsync(fullModel.MemeTags);
                await folderTagsQC.Delete.BulkDeleteAsync(fullModel.FolderTags);
            }
        }
    }
}
