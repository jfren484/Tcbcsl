﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tcbcsl.Data.Entities;
using Tcbcsl.Data.Identity;

namespace Tcbcsl.Data
{
    public class TcbcslDbContext : IdentityDbContext<TcbcslUser>
    {
        public TcbcslDbContext() : base("TcbcslDbContext", throwIfV1Schema: false) {}

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<ConferenceYear> ConferenceYears { get; set; }
        public DbSet<ContactInfoPieceType> ContactInfoPieceTypes { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<DivisionYear> DivisionYears { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameParticipant> GameParticipants { get; set; }
        public DbSet<GameStatus> GameStatuses { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<StatLine> StatLines { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamYear> TeamYears { get; set; }

        public static TcbcslDbContext Create()
        {
            return new TcbcslDbContext();
        }
    }
}