namespace Tcbcsl.Data.Migrations
{
    public partial class AddNameFieldsToUser : Migration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NameLast", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.AspNetUsers", "NameFirst", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NameFirst");
            DropColumn("dbo.AspNetUsers", "NameLast");
        }
    }
}
