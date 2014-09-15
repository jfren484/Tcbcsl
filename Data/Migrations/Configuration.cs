using System.Data.Entity.Migrations;
using Tcbcsl.Data.Entities;

namespace Tcbcsl.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TcbcslDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Tcbcsl.Data.TcbcslDbContext";
        }

        protected override void Seed(TcbcslDbContext context)
        {
            context.ContactInfoPieceTypes.AddOrUpdate(
                t => t.ContactInfoPieceTypeId,
                new ContactInfoPieceType {ContactInfoPieceTypeId = 1, Description = "Main"},
                new ContactInfoPieceType {ContactInfoPieceTypeId = 2, Description = "Mobile"},
                new ContactInfoPieceType {ContactInfoPieceTypeId = 3, Description = "Home"},
                new ContactInfoPieceType {ContactInfoPieceTypeId = 4, Description = "Work"}
                );
        }
    }
}
