namespace Tcbcsl.Data.Migrations
{
    public partial class RemoveForeignKeysFromAddress : Migration
    {
        public override void Up()
        {
            DropColumn("dbo.Addresses", "ChurchId");
            DropColumn("dbo.Addresses", "CoachId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "CoachId", c => c.Int());
            AddColumn("dbo.Addresses", "ChurchId", c => c.Int());
        }
    }
}
