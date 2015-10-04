namespace Tcbcsl.Data.Migrations
{
    public partial class MakePhoneNumberTypeRequired : Migration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", "dbo.PhoneNumberTypes");
            DropIndex("dbo.ContactPhoneNumbers", new[] { "PhoneNumberTypeId" });
            AlterColumn("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ContactPhoneNumbers", "PhoneNumberTypeId");
            AddForeignKey("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", "dbo.PhoneNumberTypes", "PhoneNumberTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", "dbo.PhoneNumberTypes");
            DropIndex("dbo.ContactPhoneNumbers", new[] { "PhoneNumberTypeId" });
            AlterColumn("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", c => c.Int());
            CreateIndex("dbo.ContactPhoneNumbers", "PhoneNumberTypeId");
            AddForeignKey("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", "dbo.PhoneNumberTypes", "PhoneNumberTypeId");
        }
    }
}
