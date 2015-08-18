using System.Data.Entity.Migrations;

namespace Tcbcsl.Data.Migrations
{
    // ReSharper disable once UnusedMember.Global
    public partial class RemoveComputedNameColumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Coaches", "FullName");
            DropColumn("dbo.Players", "FullName");
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE dbo.Coaches ADD FullName AS LTRIM(ISNULL(FirstName, '') + ' ' + ISNULL(LastName, ''))");
            Sql("ALTER TABLE dbo.Players ADD FullName AS LTRIM(ISNULL(NameFirst, '') + ' ' + ISNULL(NameLast, ''))");
        }
    }
}
