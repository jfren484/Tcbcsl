using System.Data.Entity.Migrations;

namespace Tcbcsl.Data.Migrations
{
    // ReSharper disable once UnusedMember.Global
    public partial class ChangeColumnOnGameStatus : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.GameStatus", "DisplayScores", "DisplayOutcome");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.GameStatus", "DisplayOutcome", "DisplayScores");
        }
    }
}
