using System.Data.Entity.Migrations;

namespace Tcbcsl.Data.Migrations
{
    // ReSharper disable once UnusedMember.Global
    public partial class AddTeamYearFullNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamYears", "FullName", c => c.String(maxLength: 151));
            // UPDATE ty SET ty.FullName = LTRIM(c.DisplayName + ISNULL(' ' + ty.TeamName, '')) FROM dbo.TeamYears ty JOIN dbo.Churches c ON c.ChurchId = ty.ChurchId
        }

        public override void Down()
        {
            DropColumn("dbo.TeamYears", "FullName");
        }
    }
}
