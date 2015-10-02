namespace Tcbcsl.Data.Migrations
{
    public partial class ChangeColumnOnGameStatus : Migration
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
