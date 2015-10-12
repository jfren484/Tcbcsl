namespace Tcbcsl.Data.Migrations
{
    public partial class AddPlayerForeignKeyToTeam : Migration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "CurrentTeam_TeamId", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "CurrentTeam_TeamId" });
            DropColumn("dbo.Players", "CurrentTeam_TeamId");
            CreateIndex("dbo.Players", "CurrentTeamId");
            AddForeignKey("dbo.Players", "CurrentTeamId", "dbo.Teams", "TeamId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "CurrentTeamId", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "CurrentTeamId" });
            AddColumn("dbo.Players", "CurrentTeam_TeamId", c => c.Int());
            CreateIndex("dbo.Players", "CurrentTeam_TeamId");
            AddForeignKey("dbo.Players", "CurrentTeam_TeamId", "dbo.Teams", "TeamId");
        }
    }
}
