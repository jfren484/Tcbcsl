namespace Tcbcsl.Data.Migrations
{
    public partial class AddGameLocation : Migration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Location", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Location");
        }
    }
}
