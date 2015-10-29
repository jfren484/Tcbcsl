namespace Tcbcsl.Data.Migrations
{
    public partial class AddAllowStatisticsColumn : Migration
    {
        public override void Up()
        {
            AddColumn("dbo.GameStatus", "AllowStatistics", c => c.Boolean(nullable: true));
            Sql("UPDATE dbo.GameStatus SET AllowStatistics = CASE WHEN Description = 'Final' THEN 1 ELSE 0 END");
            AlterColumn("dbo.GameStatus", "AllowStatistics", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameStatus", "AllowStatistics");
        }
    }
}
