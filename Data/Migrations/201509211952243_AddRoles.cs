using System.Data.Entity.Migrations;

namespace Tcbcsl.Data.Migrations
{
    // ReSharper disable once UnusedMember.Global
    public partial class AddRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT AspNetRoles VALUES ('SA', 'League Commissioner')");
            Sql("INSERT AspNetRoles VALUES ('Coach', 'Team Coach')");
            Sql("INSERT AspNetUserRoles SELECT u.Id, r.Id FROM AspNetUsers u, AspNetRoles r WHERE Email LIKE 'jay%'");
        }

        public override void Down()
        {
            Sql("DELETE AspNetUserRoles");
            Sql("DELETE AspNetRoles");
        }
    }
}
