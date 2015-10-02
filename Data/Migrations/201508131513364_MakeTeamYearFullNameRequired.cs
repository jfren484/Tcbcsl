namespace Tcbcsl.Data.Migrations
{
    public partial class MakeTeamYearFullNameRequired : Migration
    {
        public override void Up()
        {
            AlterColumn("dbo.TeamYears", "FullName", c => c.String(nullable: false, maxLength: 151));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeamYears", "FullName", c => c.String(maxLength: 151));
        }
    }
}
