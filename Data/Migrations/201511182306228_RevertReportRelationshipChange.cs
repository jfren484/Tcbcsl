namespace Tcbcsl.Data.Migrations
{
    public partial class RevertReportRelationshipChange : Migration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameResultReports", "TeamYearId", "dbo.TeamYears");
            DropIndex("dbo.GameResultReports", new[] { "TeamYearId" });
            AddColumn("dbo.GameResultReports", "TeamId", c => c.Int());
            CreateIndex("dbo.GameResultReports", "TeamId");
            AddForeignKey("dbo.GameResultReports", "TeamId", "dbo.Teams", "TeamId");
            DropColumn("dbo.GameResultReports", "TeamYearId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameResultReports", "TeamYearId", c => c.Int());
            DropForeignKey("dbo.GameResultReports", "TeamId", "dbo.Teams");
            DropIndex("dbo.GameResultReports", new[] { "TeamId" });
            DropColumn("dbo.GameResultReports", "TeamId");
            CreateIndex("dbo.GameResultReports", "TeamYearId");
            AddForeignKey("dbo.GameResultReports", "TeamYearId", "dbo.TeamYears", "TeamYearId");
        }
    }
}
