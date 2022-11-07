using FluentMigrator;

namespace ProjectKonsentrasi.Migrator.List;
[Migration(20221108)]
public class _20221108_AdminLogin : Migration
{
    public override void Up()
    {
        this.Create.Table("AdminUser")
            .WithColumn("ID").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("Nama").AsString().NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Password").AsString().NotNullable()
            .WithColumn("CreatedAt").AsString().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
    }

    public override void Down()
    {
        this.Delete.Table("AdminUser");
    }
}
