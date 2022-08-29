using DALQueryChain.Linq2Db.Repositories;

namespace Domain.Data.Repository
{
    //internal class MemeTagsRepository : BaseRepository<MemeRepoDbContext, MemeTag>
    //{
    //    public MemeTagsRepository(MemeRepoDbContext context) : base(context)
    //    {
    //    }

    //    protected override void OnBeforeInsert(MemeTag model)
    //    {
    //        if (model.Id == Guid.Empty)
    //            model.Id = Guid.NewGuid();
    //    }

    //    protected override Task OnBeforeInsertAsync(MemeTag model)
    //    {
    //        OnBeforeInsert(model);
    //        return Task.CompletedTask;
    //    }

    //    protected override void OnBeforeBulkInsert(IEnumerable<MemeTag> models)
    //    {
    //        foreach (var model in models)
    //        {
    //            if (model.Id == Guid.Empty)
    //                model.Id = Guid.NewGuid();
    //        }
    //    }

    //    protected override Task OnBeforeBulkInsertAsync(IEnumerable<MemeTag> models)
    //    {
    //        OnBeforeBulkInsert(models);
    //        return Task.CompletedTask;
    //    }
    //}
}
