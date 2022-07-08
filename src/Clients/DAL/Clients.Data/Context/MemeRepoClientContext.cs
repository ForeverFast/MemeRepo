using Domain.Core.Models;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace Clients.Data.Context
{
    internal class MemeRepoClientContext : DataConnection
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
