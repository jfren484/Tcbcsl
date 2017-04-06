using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Tcbcsl.Data.Entities;
using Tcbcsl.Data.Identity;

namespace Tcbcsl.Data
{
    public class TcbcslDbContext : IdentityDbContext<TcbcslUser>
    {
        public TcbcslDbContext() : base("TcbcslDbContext", throwIfV1Schema: false) { }

        public static TcbcslDbContext Create()
        {
            return new TcbcslDbContext();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Church> Churches { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<ConferenceYear> ConferenceYears { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public DbSet<ContactPhoneNumber> ContactPhoneNumbers { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<DivisionYear> DivisionYears { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameParticipant> GameParticipants { get; set; }
        public DbSet<GameResultReport> GameResultReports { get; set; }
        public DbSet<GameStatus> GameStatuses { get; set; }
        public DbSet<GameTournamentDate> GameTournamentDates { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<StatLine> StatLines { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamYear> TeamYears { get; set; }

        public int SaveChanges(string userId)
        {
            foreach (var entry in ChangeTracker.Entries<EntityCreatable>().Where(e => e.State == EntityState.Added))
            {
                entry.Entity.Created = CentralTimeZone.Now;
                entry.Entity.CreatedBy = userId;
            }

            foreach (var entry in ChangeTracker.Entries<EntityModifiable>().Where(e => e.State == EntityState.Modified))
            {
                entry.Entity.Modified = CentralTimeZone.Now;
                entry.Entity.ModifiedBy = userId;
            }

            try { return base.SaveChanges(); }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors
                               .SelectMany(err => err.ValidationErrors)
                               .Select(err => $"{err.PropertyName}: {err.ErrorMessage}");

                throw new Exception($"DbEntityValidationException: {string.Join(Environment.NewLine, errors)}");
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TcbcslUser>()
                        .HasMany(u => u.AssignedTeams)
                        .WithMany(t => t.ManagingUsers)
                        .Map(tu =>
                        {
                            tu.MapLeftKey("TcbcslUser_Id");
                            tu.MapRightKey("Team_TeamId");
                            tu.ToTable("TcbcslUserTeams");
                        });
        }
    }
}