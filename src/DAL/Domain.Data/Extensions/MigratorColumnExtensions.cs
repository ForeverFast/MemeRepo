using FluentMigrator.Builders.Create.Table;

namespace Domain.Data.Extensions
{
    internal static class MigratorColumnExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdGuidColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
            => tableWithColumnSyntax
            .WithColumn("Id")
            .AsGuid()
            .NotNullable()
            .PrimaryKey();

        public static ICreateTableColumnOptionOrWithColumnSyntax WithCreateTimeColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
          => tableWithColumnSyntax
            .WithColumn("CreatedAt").AsDateTime2();

        public static ICreateTableColumnOptionOrWithColumnSyntax WithObservebleColumns(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
         => tableWithColumnSyntax
            .WithColumn("CreatedAt").AsDateTime2()
            .WithColumn("UpdatedAt").AsDateTime2();

        public static ICreateTableColumnOptionOrWithColumnSyntax WithFolderObjectColumns(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
           => tableWithColumnSyntax
            .WithColumn("Title").AsString()
            .WithColumn("Description").AsString()
            .WithColumn("Path").AsString()
            .WithColumn("Position").AsInt64()
            .WithColumn("ParentFolderId").AsGuid().Nullable().ForeignKey("Folders", "Id");
    }
}
