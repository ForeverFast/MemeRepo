using Domain.Data.Extensions;
using FluentMigrator;

namespace Domain.Data.Migrations
{
    [Migration(1)]

    public class M000InitMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Tags")
                .WithIdGuidColumn()
                .WithCreateTimeColumn()
                .WithColumn("Title").AsString();

            Create.Table("Folders")
                .WithIdGuidColumn()
                .WithObservebleColumns()
                .WithFolderObjectColumns();

            Create.Table("FolderTags")
                .WithColumn("FolderId").AsGuid().NotNullable().ForeignKey("Folders", "Id")
                .WithColumn("TagId").AsGuid().NotNullable().ForeignKey("Tags", "Id");

            Create.UniqueConstraint("UQ_FolderId_TagId").OnTable("FolderTags").Columns("FolderId", "TagId");

            Create.Table("Memes")
                .WithIdGuidColumn()
                .WithObservebleColumns()
                .WithFolderObjectColumns();

            Create.Table("MemeTags")
               .WithColumn("MemeId").AsGuid().NotNullable().ForeignKey("Memes", "Id")
               .WithColumn("TagId").AsGuid().NotNullable().ForeignKey("Tags", "Id");

            Create.UniqueConstraint("UQ_MemeId_TagId").OnTable("MemeTags").Columns("MemeId", "TagId");
        }

        public override void Down()
        {
            Delete.UniqueConstraint("UQ_FolderId_TagId");
            Delete.UniqueConstraint("UQ_MemeId_TagId");
            Delete.Table("FolderTags");
            Delete.Table("MemeTags");
            Delete.Table("Memes");
            Delete.Table("Folders");
            Delete.Table("Tags");
        }
    }
}
