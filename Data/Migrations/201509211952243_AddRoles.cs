namespace Tcbcsl.Data.Migrations
{
    public partial class AddRoles : Migration
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
