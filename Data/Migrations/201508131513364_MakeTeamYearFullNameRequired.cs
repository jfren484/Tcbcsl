using System.Data.Entity.Migrations;

namespace Tcbcsl.Data.Migrations
{
    // ReSharper disable once UnusedMember.Global
    public partial class MakeTeamYearFullNameRequired : DbMigration
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
