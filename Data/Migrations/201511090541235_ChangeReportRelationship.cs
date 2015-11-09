namespace Tcbcsl.Data.Migrations
{
    public partial class ChangeReportRelationship : Migration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameResultReports", "TeamId", "dbo.Teams");
            DropIndex("dbo.GameResultReports", new[] { "TeamId" });
            AddColumn("dbo.GameResultReports", "TeamYearId", c => c.Int());
            CreateIndex("dbo.GameResultReports", "TeamYearId");
            AddForeignKey("dbo.GameResultReports", "TeamYearId", "dbo.TeamYears", "TeamYearId");
            DropColumn("dbo.GameResultReports", "TeamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameResultReports", "TeamId", c => c.Int());
            DropForeignKey("dbo.GameResultReports", "TeamYearId", "dbo.TeamYears");
            DropIndex("dbo.GameResultReports", new[] { "TeamYearId" });
            DropColumn("dbo.GameResultReports", "TeamYearId");
            CreateIndex("dbo.GameResultReports", "TeamId");
            AddForeignKey("dbo.GameResultReports", "TeamId", "dbo.Teams", "TeamId");
        }
    }
}
