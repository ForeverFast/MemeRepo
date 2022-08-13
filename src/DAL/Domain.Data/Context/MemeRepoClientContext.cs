using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace Domain.Data.Context
{
    public class MemeRepoClientContext : DataConnection
    {
        #region Ctors

        public MemeRepoClientContext(LinqToDBConnectionOptions<MemeRepoClientContext> options) : base(options)
        {

        }

        #endregion

        #region Areas

        internal ITable<Folder> Folders { get { return this.GetTable<Folder>(); } }
        internal ITable<Meme> Memes { get { return this.GetTable<Meme>(); } }
        internal ITable<Tag> Tags { get { return this.GetTable<Tag>(); } }

        #endregion
    }
}
