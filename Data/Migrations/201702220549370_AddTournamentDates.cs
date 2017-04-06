namespace Tcbcsl.Data.Migrations
{
    public partial class AddTournamentDates : Migration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameTournamentDates",
                c => new
                     {
                         GameTournamentDateId = c.Int(nullable: false, identity: true),
                         GameDate = c.DateTime(nullable: false),
                         CreatedBy = c.String(nullable: false, maxLength: 128),
                         Created = c.DateTime(nullable: false),
                     })
                .PrimaryKey(t => t.GameTournamentDateId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy, cascadeDelete: true)
                .Index(t => t.CreatedBy);

            Sql($"IF NOT EXISTS (SELECT TOP 1 1 FROM AspNetUsers WHERE Id = '{DbConsts.SystemUserIdString}') " +
                "INSERT AspNetUsers (Id, UserName, LastName, FirstName, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) " +
                $"VALUES ('{DbConsts.SystemUserIdString}', 'system', 'System', '', 0, 0, 0, 0, 0)");

            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2001-08-18', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2002-08-17', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2003-08-16', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2004-08-21', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2005-08-20', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2006-08-19', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2007-08-18', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2008-08-16', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2009-08-15', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2010-08-21', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2011-08-20', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2012-08-18', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2013-08-17', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2014-08-16', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2015-08-15', '{DbConsts.SystemUserIdString}', GETDATE())");
            Sql($"INSERT GameTournamentDates (GameDate, CreatedBy, Created) VALUES ('2016-08-20', '{DbConsts.SystemUserIdString}', GETDATE())");
        }

        public override void Down()
        {
            DropForeignKey("dbo.GameTournamentDates", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.GameTournamentDates", new[] {"CreatedBy"});
            DropTable("dbo.GameTournamentDates");
        }
    }
}
