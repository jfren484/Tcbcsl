namespace Tcbcsl.Data.Migrations
{
    public partial class AddWebsiteColumnToChurch : Migration
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
