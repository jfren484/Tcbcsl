namespace Tcbcsl.Data.Migrations
{
    public partial class MakePhoneNumberValueRequired : Migration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactPhoneNumbers", "PhoneNumber", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactPhoneNumbers", "PhoneNumber", c => c.String(maxLength: 20));
        }
    }
}
