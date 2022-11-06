using FluentMigrator;

namespace ProjectKonsentrasi.Migrator.List;
[Migration(20221104)]
public class _20221104_BaseDatabase : Migration
{
    public override void Up()
    {
        this.Create.Table("Initial")
            .WithColumn("id").AsInt64().NotNullable().Identity().PrimaryKey();
    }

    public override void Down()
    {
        this.Delete.Table("Initial");
    }
}
