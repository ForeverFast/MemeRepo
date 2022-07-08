using Domain.Core.Models.Base;
using LinqToDB;

namespace Domain.Data.Extensions
{
    internal static class Linq2DbExtensions
    {
        public static T Find<T>(this ITable<T> table, Guid id) where T : DomainEntity
            => table.FirstOrDefault(e => e.Id == id && e.DeletedAt == null);

        public static async Task<T> FindAsync<T>(this ITable<T> table, Guid id) where T : DomainEntity
            => await table.FirstOrDefaultAsync(e => e.Id == id && e.DeletedAt == null);

        public static int MarkAsDeleted<T>(this IDataContext dataContext, T obj,
            string tableName = null, string databaseName = null, string schemaName = null)
            where T : DomainEntity
        {
            obj.DeletedAt = DateTime.UtcNow;
            return dataContext.Update(obj, tableName, databaseName, schemaName);
        }

        public static Task<int> MarkAsDeletedAsync<T>(this IDataContext dataContext, T obj,
            string tableName = null, string databaseName = null,
            string schemaName = null, CancellationToken token = default)
            where T : DomainEntity
        {
            obj.DeletedAt = DateTime.UtcNow;
            return dataContext.UpdateAsync(obj, tableName, databaseName, schemaName, token: token);
        }

        public static int MarkAsDeleted<T>(this IQueryable<T> source) where T : DomainEntity
            => source.Set(e => e.DeletedAt, DateTime.UtcNow).Update();

        public static Task<int> MarkAsDeletedAsync<T>(this IQueryable<T> source, CancellationToken token = default) 
            where T : DomainEntity
            => source.Set(e => e.DeletedAt, DateTime.UtcNow).UpdateAsync(token);
    }
}
