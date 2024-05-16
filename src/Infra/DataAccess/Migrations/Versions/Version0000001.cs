using FluentMigrator;

namespace EasyRh.Infra.DataAccess.Migrations.Versions;

[Migration(MigrationVersion.TABLE_USER, "CREATE USER'S TABLE")]
public class Version0000001 : VersionBase
{
    public override void Up()
    {
        CreateTable("users")
            .WithColumn("Name").AsAnsiString(100).NotNullable()
            .WithColumn("Email").AsAnsiString(150).NotNullable()
            .WithColumn("Password").AsAnsiString(2000).NotNullable()
            .WithColumn("PhoneNumber").AsAnsiString(15).Nullable()
            .WithColumn("Gener").AsAnsiString(1).Nullable() 
            .WithColumn("Cpf").AsAnsiString(11).NotNullable()
            .WithColumn("DateBirth").AsDateTime().NotNullable()
            .WithColumn("HireDate").AsDateTime().Nullable()
            .WithColumn("PasswordSalt").AsAnsiString().NotNullable()
            .WithColumn("Role").AsInt16().NotNullable()
            .WithColumn("EnterpriseSerialIdentifier").AsAnsiString(50).Nullable();    
    }
}
