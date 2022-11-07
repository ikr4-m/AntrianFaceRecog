using FluentMigrator;

namespace ProjectKonsentrasi.Migrator.List;
[Migration(20221106)]
public class _20221106_DatabaseAntrian : Migration
{
    public override void Up()
    {
        this.Create.Table("DataAlamatPasien")
            .WithColumn("ID").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("Alamat").AsString().NotNullable()
            .WithColumn("RT").AsString().NotNullable()
            .WithColumn("RW").AsString().NotNullable()
            .WithColumn("Kelurahan").AsString().NotNullable()
            .WithColumn("Kecamatan").AsString().NotNullable()
            .WithColumn("Kota").AsString().NotNullable()
            .WithColumn("NoTelp").AsString().NotNullable();
        
        this.Create.Table("DataPJPasien")
            .WithColumn("ID").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("AlamatID").AsInt64().NotNullable().ForeignKey("DataAlamatPasien", "ID")
            .WithColumn("Nama").AsString().NotNullable()
            .WithColumn("NoTelp").AsString();

        this.Create.Table("ListKlinikTujuan")
            .WithColumn("ID").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("NamaKlinik").AsString().NotNullable();

        this.Create.Table("DataUmumPasien")
            .WithColumn("ID").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("AlamatID").AsInt64().NotNullable().ForeignKey("DataAlamatPasien", "ID")
            .WithColumn("KlinikTujuanID").AsInt64().NotNullable().ForeignKey("ListKlinikTujuan", "ID")
            .WithColumn("PJID").AsInt64().NotNullable().ForeignKey("DataPJPasien", "ID")
            .WithColumn("NIK").AsInt64().NotNullable()
            .WithColumn("BPJSID").AsString().Nullable()
            .WithColumn("Nama").AsString().NotNullable()
            .WithColumn("TempatLahir").AsString().NotNullable()
            .WithColumn("TanggalLahir").AsDateTime().NotNullable()
            // Laki-laki = 0
            // Perempuan = 1
            .WithColumn("JenisKelamin").AsInt16().NotNullable()
            // Islam = 0
            // Kristen = 1
            // Katolik = 2
            // Hindu = 3
            // Buddhe = 4
            // Lain-lain = 5
            .WithColumn("Agama").AsInt16().NotNullable()
            .WithColumn("StatusPerkawinan").AsInt16().NotNullable()
            .WithColumn("Pekerjaan").AsInt16().NotNullable()
            // WNA = 0
            // WNI = 1
            .WithColumn("Kewarganegarran").AsInt16().NotNullable()
            // BPJS = 0
            // Umum
            .WithColumn("MetodePembayaran").AsInt16().NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
    }

    public override void Down()
    {
        this.Delete.Table("DataUmumPasien");
        this.Delete.Table("ListKlinikPasien");
        this.Delete.Table("DataPJPasien");
        this.Delete.Table("DataAlamatPasien");
    }
}
