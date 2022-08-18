using FluentMigrator;

namespace Domain.Data.Migrations
{
    [Migration(2)]
    public class M001ChangeDescriptionMigration : Migration
    {
        public override void Up()
        {
            this.Alter.Column("Description").OnTable("Folders").AsString().Nullable();
            this.Alter.Column("Description").OnTable("Memes").AsString().Nullable();
        }

        public override void Down()
        {
            this.Alter.Column("Description").OnTable("Folders").AsString().NotNullable();
            this.Alter.Column("Description").OnTable("Memes").AsString().NotNullable();
        }
    }
}
