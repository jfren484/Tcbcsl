namespace Tcbcsl.Data.Migrations
{
    public partial class AddComputedNameColumns : Migration
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
