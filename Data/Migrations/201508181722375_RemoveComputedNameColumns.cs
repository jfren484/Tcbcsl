namespace Tcbcsl.Data.Migrations
{
    public partial class RemoveComputedNameColumns : Migration
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
