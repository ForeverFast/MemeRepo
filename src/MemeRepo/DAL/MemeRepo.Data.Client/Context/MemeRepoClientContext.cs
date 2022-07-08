using LinqToDB;
using LinqToDB.Configuration;
using MemeRepo.Core.Models;

namespace MemeRepo.Data.Client.Context
{
    internal class MemeRepoClientContext : LinqToDB.Data.DataConnection
    {
        #region Ctors

        public MemeRepoClientContext(LinqToDbConnectionOptions<MemeRepoClientContext> options) : base(options)
        {

        }

        #endregion

        #region Tables

        internal ITable<Folder> Folders { get { return this.GetTable<Folder>(); } }
        internal ITable<Meme> Memes { get { return this.GetTable<Meme>(); } }
        internal ITable<Tag> Tags { get { return this.GetTable<Tag>(); } }

        #endregion
    }
}
