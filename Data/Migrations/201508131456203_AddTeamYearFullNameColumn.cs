namespace Tcbcsl.Data.Migrations
{
    public partial class AddTeamYearFullNameColumn : Migration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamYears", "FullName", c => c.String(maxLength: 151));
        }

        public override void Down()
        {
            DropColumn("dbo.TeamYears", "FullName");
        }
    }
}
