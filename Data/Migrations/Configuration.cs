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

            context.States.AddOrUpdate(
                s => s.StateId,
                new State {StateId = 1, Abbreviation = "AL", Name = "Alabama"},
                new State {StateId = 2, Abbreviation = "AK", Name = "Alaska"},
                new State {StateId = 3, Abbreviation = "AZ", Name = "Arizona"},
                new State {StateId = 4, Abbreviation = "AR", Name = "Arkansas"},
                new State {StateId = 5, Abbreviation = "CA", Name = "California"},
                new State {StateId = 6, Abbreviation = "CO", Name = "Colorado"},
                new State {StateId = 7, Abbreviation = "CT", Name = "Connecticut"},
                new State {StateId = 8, Abbreviation = "DE", Name = "Delaware"},
                new State {StateId = 9, Abbreviation = "FL", Name = "Florida"},
                new State {StateId = 10, Abbreviation = "GA", Name = "Georgia"},
                new State {StateId = 11, Abbreviation = "HI", Name = "Hawaii"},
                new State {StateId = 12, Abbreviation = "ID", Name = "Idaho"},
                new State {StateId = 13, Abbreviation = "IL", Name = "Illinois"},
                new State {StateId = 14, Abbreviation = "IN", Name = "Indiana"},
                new State {StateId = 15, Abbreviation = "IA", Name = "Iowa"},
                new State {StateId = 16, Abbreviation = "KS", Name = "Kansas"},
                new State {StateId = 17, Abbreviation = "KY", Name = "Kentucky"},
                new State {StateId = 18, Abbreviation = "LA", Name = "Louisiana"},
                new State {StateId = 19, Abbreviation = "ME", Name = "Maine"},
                new State {StateId = 20, Abbreviation = "MD", Name = "Maryland"},
                new State {StateId = 21, Abbreviation = "MA", Name = "Massachusetts"},
                new State {StateId = 22, Abbreviation = "MI", Name = "Michigan"},
                new State {StateId = 23, Abbreviation = "MN", Name = "Minnesota"},
                new State {StateId = 24, Abbreviation = "MS", Name = "Mississippi"},
                new State {StateId = 25, Abbreviation = "MO", Name = "Missouri"},
                new State {StateId = 26, Abbreviation = "MT", Name = "Montana"},
                new State {StateId = 27, Abbreviation = "NE", Name = "Nebraska"},
                new State {StateId = 28, Abbreviation = "NV", Name = "Nevada"},
                new State {StateId = 29, Abbreviation = "NH", Name = "New Hampshire"},
                new State {StateId = 30, Abbreviation = "NJ", Name = "New Jersey"},
                new State {StateId = 31, Abbreviation = "NM", Name = "New Mexico"},
                new State {StateId = 32, Abbreviation = "NY", Name = "New York"},
                new State {StateId = 33, Abbreviation = "NC", Name = "North Carolina"},
                new State {StateId = 34, Abbreviation = "ND", Name = "North Dakota"},
                new State {StateId = 35, Abbreviation = "OH", Name = "Ohio"},
                new State {StateId = 36, Abbreviation = "OK", Name = "Oklahoma"},
                new State {StateId = 37, Abbreviation = "OR", Name = "Oregon"},
                new State {StateId = 38, Abbreviation = "PA", Name = "Pennsylvania"},
                new State {StateId = 39, Abbreviation = "RI", Name = "Rhode Island"},
                new State {StateId = 40, Abbreviation = "SC", Name = "South Carolina"},
                new State {StateId = 41, Abbreviation = "SD", Name = "South Dakota"},
                new State {StateId = 42, Abbreviation = "TN", Name = "Tennessee"},
                new State {StateId = 43, Abbreviation = "TX", Name = "Texas"},
                new State {StateId = 44, Abbreviation = "UT", Name = "Utah"},
                new State {StateId = 45, Abbreviation = "VT", Name = "Vermont"},
                new State {StateId = 46, Abbreviation = "VA", Name = "Virginia"},
                new State {StateId = 47, Abbreviation = "WA", Name = "Washington"},
                new State {StateId = 48, Abbreviation = "WV", Name = "West Virginia"},
                new State {StateId = 49, Abbreviation = "WI", Name = "Wisconsin"},
                new State {StateId = 50, Abbreviation = "WY", Name = "Wyoming"},
                new State {StateId = 51, Abbreviation = "DC", Name = "District of Columbia"},
                new State {StateId = 52, Abbreviation = "AS", Name = "American Samoa"},
                new State {StateId = 53, Abbreviation = "GU", Name = "Guam"},
                new State {StateId = 54, Abbreviation = "MP", Name = "Northern Mariana Islands[I]"},
                new State {StateId = 55, Abbreviation = "PR", Name = "Puerto Rico"},
                new State {StateId = 56, Abbreviation = "VI", Name = "U.S. Virgin Islands"}
                );
        }
    }
}
