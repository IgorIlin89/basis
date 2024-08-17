using ApiUser.Domain;
using FluentMigrator;

namespace ApiUser.Migrations;

[Migration(202408171100, "Initial")]
public class Initial : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("User")
        .WithColumn(nameof(User.Id)).AsInt64().NotNullable().Identity().PrimaryKey()
        .WithColumn("Name").AsString(10).NotNullable();
    }
}