using System.Data.Entity.Migrations;

namespace Tcbcsl.Data.Migrations
{
    // ReSharper disable once UnusedMember.Global
    public partial class AddWebsiteColumnToChurch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Churches", "Website", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Churches", "Website");
        }
    }
}
