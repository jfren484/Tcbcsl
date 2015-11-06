namespace Tcbcsl.Data.Migrations
{
    public partial class AddGameResultReportEntity : Migration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameResultReports",
                c => new
                    {
                        GameResultReportId = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        TeamId = c.Int(),
                        IsConfirmation = c.Boolean(nullable: false),
                        GameStatusId = c.Int(),
                        HomeTeamScore = c.Int(),
                        RoadTeamScore = c.Int(),
                        Note = c.String(),
                        CreatedBy = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GameResultReportId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.GameStatus", t => t.GameStatusId)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.GameId)
                .Index(t => t.TeamId)
                .Index(t => t.GameStatusId);
            
            AddColumn("dbo.Games", "IsFinalized", c => c.Boolean(nullable: false));
            Sql("UPDATE dbo.Games SET IsFinalized = CASE WHEN GameStatusId > 1 THEN 1 ELSE 0 END");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameResultReports", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.GameResultReports", "GameStatusId", "dbo.GameStatus");
            DropForeignKey("dbo.GameResultReports", "GameId", "dbo.Games");
            DropIndex("dbo.GameResultReports", new[] { "GameStatusId" });
            DropIndex("dbo.GameResultReports", new[] { "TeamId" });
            DropIndex("dbo.GameResultReports", new[] { "GameId" });
            DropColumn("dbo.Games", "IsFinalized");
            DropTable("dbo.GameResultReports");
        }
    }
}
