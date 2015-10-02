namespace Tcbcsl.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NormalizeContactInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Churches", "Phone1TypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.Churches", "Phone2TypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.Coaches", "Phone1TypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.Coaches", "Phone2TypeId", "dbo.ContactInfoPieceTypes");
            DropIndex("dbo.Churches", new[] { "Phone1TypeId" });
            DropIndex("dbo.Churches", new[] { "Phone2TypeId" });
            DropIndex("dbo.Coaches", new[] { "Phone1TypeId" });
            DropIndex("dbo.Coaches", new[] { "Phone2TypeId" });
            RenameColumn(table: "dbo.Churches", name: "StateId", newName: "State_StateId");
            RenameColumn(table: "dbo.Coaches", name: "StateId", newName: "State_StateId");
            RenameIndex(table: "dbo.Churches", name: "IX_StateId", newName: "IX_State_StateId");
            RenameIndex(table: "dbo.Coaches", name: "IX_StateId", newName: "IX_State_StateId");
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        ChurchId = c.Int(),
                        CoachId = c.Int(),
                        Street1 = c.String(maxLength: 100),
                        Street2 = c.String(maxLength: 100),
                        City = c.String(maxLength: 100),
                        StateId = c.Int(),
                        Zip = c.String(maxLength: 10),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.StateId);
            
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
                .PrimaryKey(t => t.ContactEmailAddressId)
                .ForeignKey("dbo.Churches", t => t.ChurchId)
                .ForeignKey("dbo.Coaches", t => t.CoachId)
                .ForeignKey("dbo.ContactInfoPieceTypes", t => t.ContactInfoPieceTypeId)
                .Index(t => t.ChurchId)
                .Index(t => t.CoachId)
                .Index(t => t.ContactInfoPieceTypeId);
            
            CreateTable(
                "dbo.ContactPhoneNumbers",
                c => new
                    {
                        ContactPhoneNumberId = c.Int(nullable: false, identity: true),
                        ChurchId = c.Int(),
                        CoachId = c.Int(),
                        ContactInfoPieceTypeId = c.Int(),
                        PhoneNumber = c.String(maxLength: 20),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContactPhoneNumberId)
                .ForeignKey("dbo.Churches", t => t.ChurchId)
                .ForeignKey("dbo.Coaches", t => t.CoachId)
                .ForeignKey("dbo.ContactInfoPieceTypes", t => t.ContactInfoPieceTypeId)
                .Index(t => t.ChurchId)
                .Index(t => t.CoachId)
                .Index(t => t.ContactInfoPieceTypeId);
            
            AddColumn("dbo.Churches", "AddressId", c => c.Int());
            AddColumn("dbo.Coaches", "AddressId", c => c.Int());
            CreateIndex("dbo.Churches", "AddressId");
            CreateIndex("dbo.Coaches", "AddressId");
            AddForeignKey("dbo.Churches", "AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Coaches", "AddressId", "dbo.Addresses", "AddressId");
            DropColumn("dbo.Churches", "Street1");
            DropColumn("dbo.Churches", "Street2");
            DropColumn("dbo.Churches", "City");
            DropColumn("dbo.Churches", "Zip");
            DropColumn("dbo.Churches", "Phone1");
            DropColumn("dbo.Churches", "Phone1TypeId");
            DropColumn("dbo.Churches", "Phone2");
            DropColumn("dbo.Churches", "Phone2TypeId");
            DropColumn("dbo.Churches", "Email");
            DropColumn("dbo.Coaches", "Street1");
            DropColumn("dbo.Coaches", "Street2");
            DropColumn("dbo.Coaches", "City");
            DropColumn("dbo.Coaches", "Zip");
            DropColumn("dbo.Coaches", "Phone1");
            DropColumn("dbo.Coaches", "Phone1TypeId");
            DropColumn("dbo.Coaches", "Phone2");
            DropColumn("dbo.Coaches", "Phone2TypeId");
            DropColumn("dbo.Coaches", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coaches", "Email", c => c.String(maxLength: 100));
            AddColumn("dbo.Coaches", "Phone2TypeId", c => c.Int());
            AddColumn("dbo.Coaches", "Phone2", c => c.String(maxLength: 10));
            AddColumn("dbo.Coaches", "Phone1TypeId", c => c.Int());
            AddColumn("dbo.Coaches", "Phone1", c => c.String(maxLength: 10));
            AddColumn("dbo.Coaches", "Zip", c => c.String(maxLength: 10));
            AddColumn("dbo.Coaches", "City", c => c.String(maxLength: 30));
            AddColumn("dbo.Coaches", "Street2", c => c.String(maxLength: 50));
            AddColumn("dbo.Coaches", "Street1", c => c.String(maxLength: 50));
            AddColumn("dbo.Churches", "Email", c => c.String(maxLength: 100));
            AddColumn("dbo.Churches", "Phone2TypeId", c => c.Int());
            AddColumn("dbo.Churches", "Phone2", c => c.String(maxLength: 10));
            AddColumn("dbo.Churches", "Phone1TypeId", c => c.Int());
            AddColumn("dbo.Churches", "Phone1", c => c.String(maxLength: 10));
            AddColumn("dbo.Churches", "Zip", c => c.String(maxLength: 10));
            AddColumn("dbo.Churches", "City", c => c.String(maxLength: 30));
            AddColumn("dbo.Churches", "Street2", c => c.String(maxLength: 50));
            AddColumn("dbo.Churches", "Street1", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Addresses", "StateId", "dbo.States");
            DropForeignKey("dbo.ContactEmailAddresses", "ContactInfoPieceTypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.ContactPhoneNumbers", "ContactInfoPieceTypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.ContactPhoneNumbers", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.ContactPhoneNumbers", "ChurchId", "dbo.Churches");
            DropForeignKey("dbo.ContactEmailAddresses", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.Coaches", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ContactEmailAddresses", "ChurchId", "dbo.Churches");
            DropForeignKey("dbo.Churches", "AddressId", "dbo.Addresses");
            DropIndex("dbo.ContactPhoneNumbers", new[] { "ContactInfoPieceTypeId" });
            DropIndex("dbo.ContactPhoneNumbers", new[] { "CoachId" });
            DropIndex("dbo.ContactPhoneNumbers", new[] { "ChurchId" });
            DropIndex("dbo.Coaches", new[] { "AddressId" });
            DropIndex("dbo.ContactEmailAddresses", new[] { "ContactInfoPieceTypeId" });
            DropIndex("dbo.ContactEmailAddresses", new[] { "CoachId" });
            DropIndex("dbo.ContactEmailAddresses", new[] { "ChurchId" });
            DropIndex("dbo.Churches", new[] { "AddressId" });
            DropIndex("dbo.Addresses", new[] { "StateId" });
            DropColumn("dbo.Coaches", "AddressId");
            DropColumn("dbo.Churches", "AddressId");
            DropTable("dbo.ContactPhoneNumbers");
            DropTable("dbo.ContactEmailAddresses");
            DropTable("dbo.Addresses");
            RenameIndex(table: "dbo.Coaches", name: "IX_State_StateId", newName: "IX_StateId");
            RenameIndex(table: "dbo.Churches", name: "IX_State_StateId", newName: "IX_StateId");
            RenameColumn(table: "dbo.Coaches", name: "State_StateId", newName: "StateId");
            RenameColumn(table: "dbo.Churches", name: "State_StateId", newName: "StateId");
            CreateIndex("dbo.Coaches", "Phone2TypeId");
            CreateIndex("dbo.Coaches", "Phone1TypeId");
            CreateIndex("dbo.Churches", "Phone2TypeId");
            CreateIndex("dbo.Churches", "Phone1TypeId");
            AddForeignKey("dbo.Coaches", "Phone2TypeId", "dbo.ContactInfoPieceTypes", "ContactInfoPieceTypeId");
            AddForeignKey("dbo.Coaches", "Phone1TypeId", "dbo.ContactInfoPieceTypes", "ContactInfoPieceTypeId");
            AddForeignKey("dbo.Churches", "Phone2TypeId", "dbo.ContactInfoPieceTypes", "ContactInfoPieceTypeId");
            AddForeignKey("dbo.Churches", "Phone1TypeId", "dbo.ContactInfoPieceTypes", "ContactInfoPieceTypeId");
        }
    }
}
