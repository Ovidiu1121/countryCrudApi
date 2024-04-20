using FluentMigrator;

namespace CountryCrduApi.Data.Migrations
{
    [Migration(24032024)]
    public class CreateSchema: Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Create.Table("country")
                 .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("name").AsString(128).NotNullable()
                   .WithColumn("capital").AsString(128).NotNullable()
                    .WithColumn("population").AsInt32().NotNullable();
        }
    }
}
