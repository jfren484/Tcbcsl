namespace Tcbcsl.Data.Migrations
{
    public partial class AddUserTeamRelationship : Migration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TcbcslUserTeams",
                c => new
                    {
                        TcbcslUser_Id = c.String(nullable: false, maxLength: 128),
                        Team_TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TcbcslUser_Id, t.Team_TeamId })
                .ForeignKey("dbo.AspNetUsers", t => t.TcbcslUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId, cascadeDelete: true)
                .Index(t => t.TcbcslUser_Id)
                .Index(t => t.Team_TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TcbcslUserTeams", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.TcbcslUserTeams", "TcbcslUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TcbcslUserTeams", new[] { "Team_TeamId" });
            DropIndex("dbo.TcbcslUserTeams", new[] { "TcbcslUser_Id" });
            DropTable("dbo.TcbcslUserTeams");
        }
    }
}
