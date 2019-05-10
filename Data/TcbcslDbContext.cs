using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tcbcsl.Data.Entities;
using Tcbcsl.Data.Identity;

namespace Tcbcsl.Data
{
    public class TcbcslDbContext : IdentityDbContext<TcbcslUser>
    {
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

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TcbcslUserTeam>()
                .HasKey(tut => new { tut.TcbcslUserId, tut.TeamId });
        }
    }
}