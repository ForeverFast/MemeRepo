using Domain.Data.Interfaces.QueryChain;

using LinqToDB;

using System.Linq.Expressions;

namespace Domain.Data.QueryChain
{
    internal class QueryableChain<T,F> : BaseQueryChain<T,F>, IQueryableChain<T>
        where T : class
        where F : DataConnection
    {

        private readonly List<Expression<Func<T, object>>> _loadWithList;

        internal QueryableChain(F context) : base(context, context.GetTable<T>().AsQueryable())
        {
            _loadWithList = new List<Expression<Func<T, object>>>();
        }

        private IQueryable<T> ApplyLoadWith(IQueryable<T> query)
        {
            foreach (var expression in _loadWithList)
                query = query.LoadWith(expression);

            return query;
        }

        protected override IQueryChain<T> GetQueryChain() => new QueryChain<T,F>(_context, Process());

        protected override IQueryable<T> Process()
        {
            var query = ApplyLoadWith(base.Process());

            return base.PrepareQuery(query);
        }

        public IQueryableChain<T> With(Expression<Func<T, object>> selector)
        {
            _loadWithList.Add(selector);
            return this;
        }
        

        #region Sync

        public bool Insert(T obj)
        {
            return _context.Insert(obj) == 0;
        }

        public object InsertWithIdentity(T obj)
        {
            return _context.InsertWithIdentity(obj);
        }

        public int InsertWithInt32Identity(T obj)
        {
            return _context.InsertWithInt32Identity(obj);
        }

        public bool BulkInsert(IEnumerable<T> source)
        {
            return !_context.BulkCopy(source).Abort;
        }

        public bool Update(T obj)
        {
            return _context.Update(obj) == 0;
        }

        #endregion

        #region Async

        public async Task<bool> InsertAsync(T obj)
        {
            return (await _context.InsertAsync(obj)) == 0;
        }

        public async Task<object> InsertWithIdentityAsync(T obj)
        {
            return await _context.InsertWithIdentityAsync(obj);
        }

        public async Task<int> InsertWithInt32IdentityAsync(T obj)
        {
            return await _context.InsertWithInt32IdentityAsync(obj);
        }

        public async Task<bool> BulkInsertAsync(IEnumerable<T> source)
        {
            return !(await _context.BulkCopyAsync(source)).Abort;
        }

        public async Task<bool> UpdateAsync(T obj)
        {
            return (await _context.UpdateAsync(obj)) == 0;
        }
        public async Task<bool> BulkUpdateAsync(IEnumerable<T> objs)
        {
            using (var tran = await _context.BeginTransactionAsync())
            {
                try
                {
                    foreach (var obj in objs)
                    {
                        await _context.UpdateAsync(obj);
                    }

                    await tran.CommitAsync();
                }
                catch (Exception)
                {
                    await tran.RollbackAsync();
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
