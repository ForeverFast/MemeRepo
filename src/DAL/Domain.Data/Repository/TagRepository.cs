﻿using DALQueryChain.Linq2Db.Repositories;

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
    }
}
