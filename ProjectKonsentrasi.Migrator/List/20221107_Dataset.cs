using FluentMigrator;

namespace ProjectKonsentrasi.Migrator.List;
[Migration(20221107)]
public class _20221107_Dataset : Migration
{
    public override void Up()
    {
        this.Create.Table("DatasetMukaPasien")
            .WithColumn("ID").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("DataUmumID").AsInt64().NotNullable().ForeignKey("DataUmumPasien", "ID")
            .WithColumn("IsSuccess").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);

        this.Create.Table("DatasetFile")
            .WithColumn("ID").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("DatasetMukaID").AsInt64().NotNullable().ForeignKey("DatasetMukaPasien", "ID")
            .WithColumn("Filename").AsString().NotNullable()
            .WithColumn("Confident").AsDouble().NotNullable();
    }

    public override void Down()
    {
        this.Delete.Table("DatasetFile");
        this.Delete.Table("DatasetMukaPasien");
    }
}
