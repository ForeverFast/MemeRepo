using DALQueryChain.Linq2Db.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Repository
{
    internal class FolderTagsRepository : BaseRepository<MemeRepoDbContext, FolderTag>
    {
        public FolderTagsRepository(MemeRepoDbContext context) : base(context)
        {
        }

        protected override void OnBeforeInsert(FolderTag model)
        {
            if (model.Id == Guid.Empty)
                model.Id = Guid.NewGuid();
        }

        protected override Task OnBeforeInsertAsync(FolderTag model)
        {
            OnBeforeInsert(model);
            return Task.CompletedTask;
        }

        protected override void OnBeforeBulkInsert(IEnumerable<FolderTag> models)
        {
            foreach (var model in models)
            {
                if (model.Id == Guid.Empty)
                    model.Id = Guid.NewGuid();
            }
        }

        protected override Task OnBeforeBulkInsertAsync(IEnumerable<FolderTag> models)
        {
            OnBeforeBulkInsert(models);
            return Task.CompletedTask;
        }
    }
}
