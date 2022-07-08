using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Data.Migrator.Extensions
{
    internal static class ColumnExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdGuidColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
            => tableWithColumnSyntax
            .WithColumn("Id")
            .AsGuid()
            .NotNullable()
            .PrimaryKey();

        public static ICreateTableColumnOptionOrWithColumnSyntax WithFolderObjectColumns(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
           => tableWithColumnSyntax
            .WithColumn("Title").AsString()
            .WithColumn("Description").AsString()
            .WithColumn("Path").AsString()
            .WithColumn("Position").AsInt64()
            .WithColumn("ParentFolderId").AsGuid().Nullable().ForeignKey("Folders", "Id");
    }
}
