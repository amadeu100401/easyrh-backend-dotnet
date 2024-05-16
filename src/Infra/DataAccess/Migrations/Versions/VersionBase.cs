using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace EasyRh.Infra.DataAccess.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string tableName)
    {
        return Create.Table(tableName)
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedOn").AsDateTime().NotNullable()
            .WithColumn("UpdateOn").AsDateTime().NotNullable()
            .WithColumn("Identifier").AsAnsiString().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable();
    }
}
