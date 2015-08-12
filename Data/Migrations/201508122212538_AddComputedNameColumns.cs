using System.Data.Entity.Migrations;

namespace Tcbcsl.Data.Migrations
{
    // ReSharper disable once UnusedMember.Global
    public partial class AddComputedNameColumns : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE dbo.Coaches ADD FullName AS LTRIM(ISNULL(FirstName, '') + ' ' + ISNULL(LastName, ''))");
            Sql("ALTER TABLE dbo.Players ADD FullName AS LTRIM(ISNULL(NameFirst, '') + ' ' + ISNULL(NameLast, ''))");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "FullName");
            DropColumn("dbo.Coaches", "FullName");
        }
    }
}
