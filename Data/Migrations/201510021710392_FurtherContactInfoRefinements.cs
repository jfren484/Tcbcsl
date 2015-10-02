namespace Tcbcsl.Data.Migrations
{
    public partial class FurtherContactInfoRefinements : Migration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactEmailAddresses", "ChurchId", "dbo.Churches");
            DropForeignKey("dbo.ContactEmailAddresses", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.ContactPhoneNumbers", "ContactInfoPieceTypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.ContactEmailAddresses", "ContactInfoPieceTypeId", "dbo.ContactInfoPieceTypes");
            DropIndex("dbo.ContactEmailAddresses", new[] { "ChurchId" });
            DropIndex("dbo.ContactEmailAddresses", new[] { "CoachId" });
            DropIndex("dbo.ContactEmailAddresses", new[] { "ContactInfoPieceTypeId" });
            DropIndex("dbo.ContactPhoneNumbers", new[] { "ContactInfoPieceTypeId" });
            CreateTable(
                "dbo.PhoneNumberTypes",
                c => new
                    {
                        PhoneNumberTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PhoneNumberTypeId);
            
            AddColumn("dbo.Churches", "EmailAddress", c => c.String(maxLength: 100));
            AddColumn("dbo.Coaches", "EmailAddress", c => c.String(maxLength: 100));
            AddColumn("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", c => c.Int());
            CreateIndex("dbo.ContactPhoneNumbers", "PhoneNumberTypeId");
            AddForeignKey("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", "dbo.PhoneNumberTypes", "PhoneNumberTypeId");
            DropColumn("dbo.ContactPhoneNumbers", "ContactInfoPieceTypeId");
            DropTable("dbo.ContactEmailAddresses");
            DropTable("dbo.ContactInfoPieceTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContactInfoPieceTypes",
                c => new
                    {
                        ContactInfoPieceTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ContactInfoPieceTypeId);
            
            CreateTable(
                "dbo.ContactEmailAddresses",
                c => new
                    {
                        ContactEmailAddressId = c.Int(nullable: false, identity: true),
                        ChurchId = c.Int(),
                        CoachId = c.Int(),
                        ContactInfoPieceTypeId = c.Int(),
                        EmailAddress = c.String(maxLength: 100),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContactEmailAddressId);
            
            AddColumn("dbo.ContactPhoneNumbers", "ContactInfoPieceTypeId", c => c.Int());
            DropForeignKey("dbo.ContactPhoneNumbers", "PhoneNumberTypeId", "dbo.PhoneNumberTypes");
            DropIndex("dbo.ContactPhoneNumbers", new[] { "PhoneNumberTypeId" });
            DropColumn("dbo.ContactPhoneNumbers", "PhoneNumberTypeId");
            DropColumn("dbo.Coaches", "EmailAddress");
            DropColumn("dbo.Churches", "EmailAddress");
            DropTable("dbo.PhoneNumberTypes");
            CreateIndex("dbo.ContactPhoneNumbers", "ContactInfoPieceTypeId");
            CreateIndex("dbo.ContactEmailAddresses", "ContactInfoPieceTypeId");
            CreateIndex("dbo.ContactEmailAddresses", "CoachId");
            CreateIndex("dbo.ContactEmailAddresses", "ChurchId");
            AddForeignKey("dbo.ContactEmailAddresses", "ContactInfoPieceTypeId", "dbo.ContactInfoPieceTypes", "ContactInfoPieceTypeId");
            AddForeignKey("dbo.ContactPhoneNumbers", "ContactInfoPieceTypeId", "dbo.ContactInfoPieceTypes", "ContactInfoPieceTypeId");
            AddForeignKey("dbo.ContactEmailAddresses", "CoachId", "dbo.Coaches", "CoachId");
            AddForeignKey("dbo.ContactEmailAddresses", "ChurchId", "dbo.Churches", "ChurchId");
        }
    }
}
