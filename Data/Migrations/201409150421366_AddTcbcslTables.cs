namespace Tcbcsl.Data.Migrations
{
    public partial class AddTcbcslTables : Migration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        CoachId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 30),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        Comments = c.String(),
                        Street1 = c.String(maxLength: 50),
                        Street2 = c.String(maxLength: 50),
                        City = c.String(maxLength: 30),
                        StateId = c.Int(),
                        Zip = c.String(maxLength: 10),
                        Phone1 = c.String(maxLength: 10),
                        Phone1TypeId = c.Int(),
                        Phone2 = c.String(maxLength: 10),
                        Phone2TypeId = c.Int(),
                        Email = c.String(maxLength: 100),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.CoachId)
                .ForeignKey("dbo.ContactInfoPieceTypes", t => t.Phone1TypeId)
                .ForeignKey("dbo.ContactInfoPieceTypes", t => t.Phone2TypeId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.StateId)
                .Index(t => t.Phone1TypeId)
                .Index(t => t.Phone2TypeId);
            
            CreateTable(
                "dbo.ContactInfoPieceTypes",
                c => new
                    {
                        ContactInfoPieceTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ContactInfoPieceTypeId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Abbreviation = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.Churches",
                c => new
                    {
                        ChurchId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        DisplayName = c.String(nullable: false, maxLength: 100),
                        Information = c.String(),
                        Street1 = c.String(maxLength: 50),
                        Street2 = c.String(maxLength: 50),
                        City = c.String(maxLength: 30),
                        StateId = c.Int(),
                        Zip = c.String(maxLength: 10),
                        Phone1 = c.String(maxLength: 10),
                        Phone1TypeId = c.Int(),
                        Phone2 = c.String(maxLength: 10),
                        Phone2TypeId = c.Int(),
                        Email = c.String(maxLength: 100),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ChurchId)
                .ForeignKey("dbo.ContactInfoPieceTypes", t => t.Phone1TypeId)
                .ForeignKey("dbo.ContactInfoPieceTypes", t => t.Phone2TypeId)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.StateId)
                .Index(t => t.Phone1TypeId)
                .Index(t => t.Phone2TypeId);
            
            CreateTable(
                "dbo.TeamYears",
                c => new
                    {
                        TeamYearId = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        TeamName = c.String(maxLength: 50),
                        DivisionYearId = c.Int(nullable: false),
                        ChurchId = c.Int(nullable: false),
                        HeadCoachId = c.Int(nullable: false),
                        KeepsStats = c.Boolean(nullable: false),
                        HasPaid = c.Boolean(nullable: false),
                        Clinch = c.String(maxLength: 5),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.TeamYearId)
                .ForeignKey("dbo.Churches", t => t.ChurchId, cascadeDelete: true)
                .ForeignKey("dbo.DivisionYears", t => t.DivisionYearId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Coaches", t => t.HeadCoachId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.DivisionYearId)
                .Index(t => t.ChurchId)
                .Index(t => t.HeadCoachId);
            
            CreateTable(
                "dbo.DivisionYears",
                c => new
                    {
                        DivisionYearId = c.Int(nullable: false, identity: true),
                        DivisionId = c.Int(nullable: false),
                        ConferenceYearId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        IsInLeague = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.DivisionYearId)
                .ForeignKey("dbo.ConferenceYears", t => t.ConferenceYearId, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.DivisionId)
                .Index(t => t.ConferenceYearId);
            
            CreateTable(
                "dbo.ConferenceYears",
                c => new
                    {
                        ConferenceYearId = c.Int(nullable: false, identity: true),
                        ConferenceId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        IsInLeague = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ConferenceYearId)
                .ForeignKey("dbo.Conferences", t => t.ConferenceId, cascadeDelete: true)
                .Index(t => t.ConferenceId);
            
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        ConferenceId = c.Int(nullable: false, identity: true),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ConferenceId);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        DivisionId = c.Int(nullable: false, identity: true),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.DivisionId);
            
            CreateTable(
                "dbo.GameParticipants",
                c => new
                    {
                        GameParticipantId = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        TeamYearId = c.Int(nullable: false),
                        IsHost = c.Boolean(nullable: false),
                        RunsScored = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.GameParticipantId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.TeamYears", t => t.TeamYearId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.TeamYearId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameDate = c.DateTime(nullable: false),
                        GameTypeId = c.Int(nullable: false),
                        GameStatusId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.GameStatus", t => t.GameStatusId, cascadeDelete: true)
                .ForeignKey("dbo.GameTypes", t => t.GameTypeId, cascadeDelete: true)
                .Index(t => t.GameTypeId)
                .Index(t => t.GameStatusId);
            
            CreateTable(
                "dbo.GameStatus",
                c => new
                    {
                        GameStatusId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        DisplayScores = c.Boolean(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GameStatusId);
            
            CreateTable(
                "dbo.GameTypes",
                c => new
                    {
                        GameTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        RecordGame = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GameTypeId);
            
            CreateTable(
                "dbo.StatLines",
                c => new
                    {
                        StatLineId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        GameParticipantId = c.Int(nullable: false),
                        BattingOrderPosition = c.Int(nullable: false),
                        StatSingles = c.Int(nullable: false),
                        StatDoubles = c.Int(nullable: false),
                        StatTriples = c.Int(nullable: false),
                        StatHomeRuns = c.Int(nullable: false),
                        StatWalks = c.Int(nullable: false),
                        StatSacrificeFlies = c.Int(nullable: false),
                        StatOuts = c.Int(nullable: false),
                        StatFieldersChoices = c.Int(nullable: false),
                        StatReachedByErrors = c.Int(nullable: false),
                        StatStrikeouts = c.Int(nullable: false),
                        StatRuns = c.Int(nullable: false),
                        StatRunsBattedIn = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.StatLineId)
                .ForeignKey("dbo.GameParticipants", t => t.GameParticipantId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.GameParticipantId);

            Sql("ALTER TABLE dbo.StatLines ADD StatHits AS StatSingles + StatDoubles + StatTriples + StatHomeRuns");
            Sql("ALTER TABLE dbo.StatLines ADD StatTotalBases AS StatSingles + StatDoubles * 2 + StatTriples * 3 + StatHomeRuns * 4");
            Sql("ALTER TABLE dbo.StatLines ADD StatAtBats AS StatSingles + StatDoubles + StatTriples + StatHomeRuns + StatOuts + StatFieldersChoices + StatReachedByErrors");
            Sql("ALTER TABLE dbo.StatLines ADD StatPlateAppearances AS StatSingles + StatDoubles + StatTriples + StatHomeRuns + StatOuts + StatFieldersChoices + StatReachedByErrors + StatWalks + StatSacrificeFlies");

            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        NameLast = c.String(nullable: false, maxLength: 30),
                        NameFirst = c.String(nullable: false, maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        CurrentTeamId = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                        CurrentTeam_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Teams", t => t.CurrentTeam_TeamId)
                .Index(t => t.CurrentTeam_TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        FieldInformation = c.String(),
                        Comments = c.String(),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.TeamId);
            
            CreateTable(
                "dbo.NewsItems",
                c => new
                    {
                        NewsItemId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TeamId = c.Int(),
                        Subject = c.String(maxLength: 255),
                        Content = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.NewsItemId)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.PageContents",
                c => new
                    {
                        PageContentId = c.Int(nullable: false, identity: true),
                        PageTag = c.String(nullable: false, maxLength: 30),
                        Content = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.PageContentId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coaches", "StateId", "dbo.States");
            DropForeignKey("dbo.TeamYears", "HeadCoachId", "dbo.Coaches");
            DropForeignKey("dbo.GameParticipants", "TeamYearId", "dbo.TeamYears");
            DropForeignKey("dbo.StatLines", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.TeamYears", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Players", "CurrentTeam_TeamId", "dbo.Teams");
            DropForeignKey("dbo.NewsItems", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.StatLines", "GameParticipantId", "dbo.GameParticipants");
            DropForeignKey("dbo.Games", "GameTypeId", "dbo.GameTypes");
            DropForeignKey("dbo.Games", "GameStatusId", "dbo.GameStatus");
            DropForeignKey("dbo.GameParticipants", "GameId", "dbo.Games");
            DropForeignKey("dbo.TeamYears", "DivisionYearId", "dbo.DivisionYears");
            DropForeignKey("dbo.DivisionYears", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.DivisionYears", "ConferenceYearId", "dbo.ConferenceYears");
            DropForeignKey("dbo.ConferenceYears", "ConferenceId", "dbo.Conferences");
            DropForeignKey("dbo.TeamYears", "ChurchId", "dbo.Churches");
            DropForeignKey("dbo.Churches", "StateId", "dbo.States");
            DropForeignKey("dbo.Churches", "Phone2TypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.Churches", "Phone1TypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.Coaches", "Phone2TypeId", "dbo.ContactInfoPieceTypes");
            DropForeignKey("dbo.Coaches", "Phone1TypeId", "dbo.ContactInfoPieceTypes");
            DropIndex("dbo.NewsItems", new[] { "TeamId" });
            DropIndex("dbo.Players", new[] { "CurrentTeam_TeamId" });
            DropIndex("dbo.StatLines", new[] { "GameParticipantId" });
            DropIndex("dbo.StatLines", new[] { "PlayerId" });
            DropIndex("dbo.Games", new[] { "GameStatusId" });
            DropIndex("dbo.Games", new[] { "GameTypeId" });
            DropIndex("dbo.GameParticipants", new[] { "TeamYearId" });
            DropIndex("dbo.GameParticipants", new[] { "GameId" });
            DropIndex("dbo.ConferenceYears", new[] { "ConferenceId" });
            DropIndex("dbo.DivisionYears", new[] { "ConferenceYearId" });
            DropIndex("dbo.DivisionYears", new[] { "DivisionId" });
            DropIndex("dbo.TeamYears", new[] { "HeadCoachId" });
            DropIndex("dbo.TeamYears", new[] { "ChurchId" });
            DropIndex("dbo.TeamYears", new[] { "DivisionYearId" });
            DropIndex("dbo.TeamYears", new[] { "TeamId" });
            DropIndex("dbo.Churches", new[] { "Phone2TypeId" });
            DropIndex("dbo.Churches", new[] { "Phone1TypeId" });
            DropIndex("dbo.Churches", new[] { "StateId" });
            DropIndex("dbo.Coaches", new[] { "Phone2TypeId" });
            DropIndex("dbo.Coaches", new[] { "Phone1TypeId" });
            DropIndex("dbo.Coaches", new[] { "StateId" });
            DropTable("dbo.PageContents");
            DropTable("dbo.NewsItems");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.StatLines");
            DropTable("dbo.GameTypes");
            DropTable("dbo.GameStatus");
            DropTable("dbo.Games");
            DropTable("dbo.GameParticipants");
            DropTable("dbo.Divisions");
            DropTable("dbo.Conferences");
            DropTable("dbo.ConferenceYears");
            DropTable("dbo.DivisionYears");
            DropTable("dbo.TeamYears");
            DropTable("dbo.Churches");
            DropTable("dbo.States");
            DropTable("dbo.ContactInfoPieceTypes");
            DropTable("dbo.Coaches");
        }
    }
}
