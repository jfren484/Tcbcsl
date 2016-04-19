namespace Tcbcsl.Data.Migrations
{
    public partial class EliminateSomeMaxLengthFields : Migration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.Addresses", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Churches", "Website", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Churches", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.Churches", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.ContactPhoneNumbers", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.ContactPhoneNumbers", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Coaches", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.Coaches", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.TeamYears", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.TeamYears", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.DivisionYears", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.DivisionYears", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.ConferenceYears", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.ConferenceYears", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Conferences", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.Conferences", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Divisions", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.Divisions", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.GameParticipants", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.GameParticipants", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Games", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.Games", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.GameResultReports", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Teams", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.Teams", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.NewsItems", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.NewsItems", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Players", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.Players", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.StatLines", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.StatLines", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.PageContents", "ModifiedBy", c => c.String(maxLength: 200));
            AlterColumn("dbo.PageContents", "CreatedBy", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PageContents", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.PageContents", "ModifiedBy", c => c.String());
            AlterColumn("dbo.StatLines", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.StatLines", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Players", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Players", "ModifiedBy", c => c.String());
            AlterColumn("dbo.NewsItems", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.NewsItems", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Teams", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Teams", "ModifiedBy", c => c.String());
            AlterColumn("dbo.GameResultReports", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Games", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Games", "ModifiedBy", c => c.String());
            AlterColumn("dbo.GameParticipants", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.GameParticipants", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Divisions", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Divisions", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Conferences", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Conferences", "ModifiedBy", c => c.String());
            AlterColumn("dbo.ConferenceYears", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.ConferenceYears", "ModifiedBy", c => c.String());
            AlterColumn("dbo.DivisionYears", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.DivisionYears", "ModifiedBy", c => c.String());
            AlterColumn("dbo.TeamYears", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.TeamYears", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Coaches", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Coaches", "ModifiedBy", c => c.String());
            AlterColumn("dbo.ContactPhoneNumbers", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.ContactPhoneNumbers", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Churches", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Churches", "ModifiedBy", c => c.String());
            AlterColumn("dbo.Churches", "Website", c => c.String());
            AlterColumn("dbo.Addresses", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "ModifiedBy", c => c.String());
        }
    }
}
