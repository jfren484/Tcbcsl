using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tcbcsl.Data.Migrations
{
    public partial class InitialEFCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetRoles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,] {
                    { "SA", "League Commissioner" },
                    { "Coach", "Team Coach" }
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEndDateUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false, defaultValueSql: "''"),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStatus",
                columns: table => new
                {
                    GameStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayOutcome = table.Column<bool>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    AllowStatistics = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.GameStatus", x => x.GameStatusId);
                });

            migrationBuilder.InsertData(
                table: "GameStatus",
                columns: new[] { "GameStatusId", "Description", "DisplayOutcome", "IsComplete", "AllowStatistics" },
                values: new object[,] {
                    { 1, "Scheduled", false, false, false },
                    { 2, "Postponed", true, false, false },
                    { 3, "Rained Out", true, false, false },
                    { 4, "Forfeited", true, true, false },
                    { 5, "Final", true, true, true }
                });

            migrationBuilder.CreateTable(
                name: "GameTypes",
                columns: table => new
                {
                    GameTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    RecordGame = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.GameTypes", x => x.GameTypeId);
                });

            migrationBuilder.InsertData(
                table: "GameTypes",
                columns: new[] { "GameTypeId", "Description", "RecordGame" },
                values: new object[,] {
                    { 1, "Regular Season", true },
                    { 2, "Post-Season", false },
                    { 3, "Exhibition", false },
                    { 4, "Game Placeholder", false }
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberTypes",
                columns: table => new
                {
                    PhoneNumberTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.PhoneNumberTypes", x => x.PhoneNumberTypeId);
                });

            migrationBuilder.InsertData(
                table: "PhoneNumberTypes",
                columns: new[] { "PhoneNumberTypeId", "Description" },
                values: new object[,] {
                    { 1, "Main" },
                    { 2, "Mobile" },
                    { 3, "Home" },
                    { 4, "Work" }
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abbreviation = table.Column<string>(maxLength: 2, nullable: false, type: "char(2)"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.States", x => x.StateId);
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "Abbreviation", "Name" },
                values: new object[,] {
                    { 1, "AL", "Alabama" },
                    { 2, "AK", "Alaska" },
                    { 3, "AZ", "Arizona" },
                    { 4, "AR", "Arkansas" },
                    { 5, "CA", "California" },
                    { 6, "CO", "Colorado" },
                    { 7, "CT", "Connecticut" },
                    { 8, "DE", "Delaware" },
                    { 9, "FL", "Florida" },
                    { 10, "GA", "Georgia" },
                    { 11, "HI", "Hawaii" },
                    { 12, "ID", "Idaho" },
                    { 13, "IL", "Illinois" },
                    { 14, "IN", "Indiana" },
                    { 15, "IA", "Iowa" },
                    { 16, "KS", "Kansas" },
                    { 17, "KY", "Kentucky" },
                    { 18, "LA", "Louisiana" },
                    { 19, "ME", "Maine" },
                    { 20, "MD", "Maryland" },
                    { 21, "MA", "Massachusetts" },
                    { 22, "MI", "Michigan" },
                    { 23, "MN", "Minnesota" },
                    { 24, "MS", "Mississippi" },
                    { 25, "MO", "Missouri" },
                    { 26, "MT", "Montana" },
                    { 27, "NE", "Nebraska" },
                    { 28, "NV", "Nevada" },
                    { 29, "NH", "New Hampshire" },
                    { 30, "NJ", "New Jersey" },
                    { 31, "NM", "New Mexico" },
                    { 32, "NY", "New York" },
                    { 33, "NC", "North Carolina" },
                    { 34, "ND", "North Dakota" },
                    { 35, "OH", "Ohio" },
                    { 36, "OK", "Oklahoma" },
                    { 37, "OR", "Oregon" },
                    { 38, "PA", "Pennsylvania" },
                    { 39, "RI", "Rhode Island" },
                    { 40, "SC", "South Carolina" },
                    { 41, "SD", "South Dakota" },
                    { 42, "TN", "Tennessee" },
                    { 43, "TX", "Texas" },
                    { 44, "UT", "Utah" },
                    { 45, "VT", "Vermont" },
                    { 46, "VA", "Virginia" },
                    { 47, "WA", "Washington" },
                    { 48, "WV", "West Virginia" },
                    { 49, "WI", "Wisconsin" },
                    { 50, "WY", "Wyoming" },
                    { 51, "DC", "District of Columbia" },
                    { 52, "AS", "American Samoa" },
                    { 53, "GU", "Guam" },
                    { 54, "MP", "Northern Mariana Islands[I]" },
                    { 55, "PR", "Puerto Rico" },
                    { 56, "VI", "U.S. Virgin Islands" }
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(maxLength: 128, nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    UserId = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey, x.UserId });
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 128, nullable: false),
                    RoleId = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    ConferenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Conferences", x => x.ConferenceId);
                    table.ForeignKey(
                        name: "FK_dbo.Conferences_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Conferences_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    DivisionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Divisions", x => x.DivisionId);
                    table.ForeignKey(
                        name: "FK_dbo.Divisions_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Divisions_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameTournamentDates",
                columns: table => new
                {
                    GameTournamentDateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.GameTournamentDates", x => x.GameTournamentDateId);
                    table.ForeignKey(
                        name: "FK_dbo.GameTournamentDates_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageContents",
                columns: table => new
                {
                    PageContentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PageTag = table.Column<string>(maxLength: 30, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.PageContents", x => x.PageContentId);
                    table.ForeignKey(
                        name: "FK_dbo.PageContents_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.PageContents_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FieldInformation = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_dbo.Teams_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Teams_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    GameTypeId = table.Column<int>(nullable: false),
                    GameStatusId = table.Column<int>(nullable: false),
                    IsFinalized = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_dbo.Games_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Games_dbo.GameStatus_GameStatusId",
                        column: x => x.GameStatusId,
                        principalTable: "GameStatus",
                        principalColumn: "GameStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Games_dbo.GameTypes_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "GameTypes",
                        principalColumn: "GameTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Games_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Street1 = table.Column<string>(maxLength: 100, nullable: true),
                    Street2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    Zip = table.Column<string>(maxLength: 10, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_dbo.Addresses_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Addresses_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Addresses_dbo.States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceYears",
                columns: table => new
                {
                    ConferenceYearId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConferenceId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    IsInLeague = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ConferenceYears", x => x.ConferenceYearId);
                    table.ForeignKey(
                        name: "FK_dbo.ConferenceYears_dbo.Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "ConferenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.ConferenceYears_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ConferenceYears_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsItems",
                columns: table => new
                {
                    NewsItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    Subject = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.NewsItems", x => x.NewsItemId);
                    table.ForeignKey(
                        name: "FK_dbo.NewsItems_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.NewsItems_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.NewsItems_dbo.Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CurrentTeamId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_dbo.Players_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Players_dbo.Teams_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Players_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TcbcslUserTeams",
                columns: table => new
                {
                    TcbcslUser_Id = table.Column<string>(maxLength: 128, nullable: false),
                    Team_TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TcbcslUserTeams", x => new { x.TcbcslUser_Id, x.Team_TeamId });
                    table.ForeignKey(
                        name: "FK_dbo.TcbcslUserTeams_dbo.Teams_Team_TeamId",
                        column: x => x.Team_TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.TcbcslUserTeams_dbo.AspNetUsers_TcbcslUser_Id",
                        column: x => x.TcbcslUser_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameResultReports",
                columns: table => new
                {
                    GameResultReportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<int>(nullable: false),
                    IsConfirmation = table.Column<bool>(nullable: false),
                    GameStatusId = table.Column<int>(nullable: true),
                    HomeTeamScore = table.Column<int>(nullable: true),
                    RoadTeamScore = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.GameResultReports", x => x.GameResultReportId);
                    table.ForeignKey(
                        name: "FK_dbo.GameResultReports_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.GameResultReports_dbo.Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.GameResultReports_dbo.GameStatus_GameStatusId",
                        column: x => x.GameStatusId,
                        principalTable: "GameStatus",
                        principalColumn: "GameStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.GameResultReports_dbo.Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Churches",
                columns: table => new
                {
                    ChurchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: false),
                    Information = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Website = table.Column<string>(maxLength: 1000, nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Churches", x => x.ChurchId);
                    table.ForeignKey(
                        name: "FK_dbo.Churches_dbo.Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Churches_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Churches_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    CoachId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Coaches", x => x.CoachId);
                    table.ForeignKey(
                        name: "FK_dbo.Coaches_dbo.Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Coaches_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Coaches_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DivisionYears",
                columns: table => new
                {
                    DivisionYearId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DivisionId = table.Column<int>(nullable: false),
                    ConferenceYearId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    IsInLeague = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.DivisionYears", x => x.DivisionYearId);
                    table.ForeignKey(
                        name: "FK_dbo.DivisionYears_dbo.ConferenceYears_ConferenceYearId",
                        column: x => x.ConferenceYearId,
                        principalTable: "ConferenceYears",
                        principalColumn: "ConferenceYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.DivisionYears_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.DivisionYears_dbo.Divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Divisions",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.DivisionYears_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactPhoneNumbers",
                columns: table => new
                {
                    ContactPhoneNumberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChurchId = table.Column<int>(nullable: true),
                    CoachId = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    PhoneNumberTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.ContactPhoneNumbers", x => x.ContactPhoneNumberId);
                    table.ForeignKey(
                        name: "FK_dbo.ContactPhoneNumbers_dbo.Churches_ChurchId",
                        column: x => x.ChurchId,
                        principalTable: "Churches",
                        principalColumn: "ChurchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ContactPhoneNumbers_dbo.Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "CoachId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ContactPhoneNumbers_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ContactPhoneNumbers_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.ContactPhoneNumbers_dbo.PhoneNumberTypes_PhoneNumberTypeId",
                        column: x => x.PhoneNumberTypeId,
                        principalTable: "PhoneNumberTypes",
                        principalColumn: "PhoneNumberTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamYears",
                columns: table => new
                {
                    TeamYearId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    TeamName = table.Column<string>(maxLength: 50, nullable: true),
                    DivisionYearId = table.Column<int>(nullable: false),
                    ChurchId = table.Column<int>(nullable: false),
                    HeadCoachId = table.Column<int>(nullable: false),
                    KeepsStats = table.Column<bool>(nullable: false),
                    HasPaid = table.Column<bool>(nullable: false),
                    Clinch = table.Column<string>(maxLength: 5, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    FullName = table.Column<string>(maxLength: 151, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.TeamYears", x => x.TeamYearId);
                    table.ForeignKey(
                        name: "FK_dbo.TeamYears_dbo.Churches_ChurchId",
                        column: x => x.ChurchId,
                        principalTable: "Churches",
                        principalColumn: "ChurchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.TeamYears_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.TeamYears_dbo.DivisionYears_DivisionYearId",
                        column: x => x.DivisionYearId,
                        principalTable: "DivisionYears",
                        principalColumn: "DivisionYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.TeamYears_dbo.Coaches_HeadCoachId",
                        column: x => x.HeadCoachId,
                        principalTable: "Coaches",
                        principalColumn: "CoachId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.TeamYears_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.TeamYears_dbo.Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameParticipants",
                columns: table => new
                {
                    GameParticipantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<int>(nullable: false),
                    TeamYearId = table.Column<int>(nullable: false),
                    IsHost = table.Column<bool>(nullable: false),
                    RunsScored = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.GameParticipants", x => x.GameParticipantId);
                    table.ForeignKey(
                        name: "FK_dbo.GameParticipants_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.GameParticipants_dbo.Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.GameParticipants_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.GameParticipants_dbo.TeamYears_TeamYearId",
                        column: x => x.TeamYearId,
                        principalTable: "TeamYears",
                        principalColumn: "TeamYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatLines",
                columns: table => new
                {
                    StatLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    GameParticipantId = table.Column<int>(nullable: false),
                    BattingOrderPosition = table.Column<int>(nullable: false),
                    StatSingles = table.Column<int>(nullable: false),
                    StatDoubles = table.Column<int>(nullable: false),
                    StatTriples = table.Column<int>(nullable: false),
                    StatHomeRuns = table.Column<int>(nullable: false),
                    StatWalks = table.Column<int>(nullable: false),
                    StatSacrificeFlies = table.Column<int>(nullable: false),
                    StatOuts = table.Column<int>(nullable: false),
                    StatFieldersChoices = table.Column<int>(nullable: false),
                    StatReachedByErrors = table.Column<int>(nullable: false),
                    StatStrikeouts = table.Column<int>(nullable: false),
                    StatRuns = table.Column<int>(nullable: false),
                    StatRunsBattedIn = table.Column<int>(nullable: false),
                    StatHits = table.Column<int>(nullable: false,
                        computedColumnSql: "StatSingles + StatDoubles + StatTriples + StatHomeRuns"),
                    StatTotalBases = table.Column<int>(nullable: false,
                        computedColumnSql: "StatSingles + StatDoubles * 2 + StatTriples * 3 + StatHomeRuns * 4"),
                    StatAtBats = table.Column<int>(nullable: false,
                        computedColumnSql: "StatSingles + StatDoubles + StatTriples + StatHomeRuns + StatOuts + StatFieldersChoices + StatReachedByErrors"),
                    StatPlateAppearances = table.Column<int>(nullable: false,
                        computedColumnSql: "StatSingles + StatDoubles + StatTriples + StatHomeRuns + StatOuts + StatFieldersChoices + StatReachedByErrors + StatWalks + StatSacrificeFlies"),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.StatLines", x => x.StatLineId);
                    table.ForeignKey(
                        name: "FK_dbo.StatLines_dbo.AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.StatLines_dbo.GameParticipants_GameParticipantId",
                        column: x => x.GameParticipantId,
                        principalTable: "GameParticipants",
                        principalColumn: "GameParticipantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.StatLines_dbo.AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.StatLines_dbo.Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "Addresses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "Addresses",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StateId",
                table: "Addresses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AddressId",
                table: "Churches",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "Churches",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "Churches",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AddressId",
                table: "Coaches",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "Coaches",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "Coaches",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "Conferences",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "Conferences",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceId",
                table: "ConferenceYears",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "ConferenceYears",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "ConferenceYears",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchId",
                table: "ContactPhoneNumbers",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachId",
                table: "ContactPhoneNumbers",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "ContactPhoneNumbers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "ContactPhoneNumbers",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumberTypeId",
                table: "ContactPhoneNumbers",
                column: "PhoneNumberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "Divisions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "Divisions",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceYearId",
                table: "DivisionYears",
                column: "ConferenceYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "DivisionYears",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionId",
                table: "DivisionYears",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "DivisionYears",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "GameParticipants",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GameId",
                table: "GameParticipants",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "GameParticipants",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TeamYearId",
                table: "GameParticipants",
                column: "TeamYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "GameResultReports",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GameId",
                table: "GameResultReports",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatusId",
                table: "GameResultReports",
                column: "GameStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamId",
                table: "GameResultReports",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "Games",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GameStatusId",
                table: "Games",
                column: "GameStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTypeId",
                table: "Games",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "Games",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "GameTournamentDates",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "NewsItems",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "NewsItems",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TeamId",
                table: "NewsItems",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "PageContents",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "PageContents",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "Players",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentTeamId",
                table: "Players",
                column: "CurrentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "Players",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "StatLines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GameParticipantId",
                table: "StatLines",
                column: "GameParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "StatLines",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerId",
                table: "StatLines",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TcbcslUser_Id",
                table: "TcbcslUserTeams",
                column: "TcbcslUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TeamId",
                table: "TcbcslUserTeams",
                column: "Team_TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "Teams",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "Teams",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchId",
                table: "TeamYears",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedBy",
                table: "TeamYears",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionYearId",
                table: "TeamYears",
                column: "DivisionYearId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadCoachId",
                table: "TeamYears",
                column: "HeadCoachId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifiedBy",
                table: "TeamYears",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TeamId",
                table: "TeamYears",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "ContactPhoneNumbers");

            migrationBuilder.DropTable(
                name: "GameResultReports");

            migrationBuilder.DropTable(
                name: "GameTournamentDates");

            migrationBuilder.DropTable(
                name: "NewsItems");

            migrationBuilder.DropTable(
                name: "PageContents");

            migrationBuilder.DropTable(
                name: "StatLines");

            migrationBuilder.DropTable(
                name: "TcbcslUserTeams");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PhoneNumberTypes");

            migrationBuilder.DropTable(
                name: "GameParticipants");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "TeamYears");

            migrationBuilder.DropTable(
                name: "GameStatus");

            migrationBuilder.DropTable(
                name: "GameTypes");

            migrationBuilder.DropTable(
                name: "Churches");

            migrationBuilder.DropTable(
                name: "DivisionYears");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "ConferenceYears");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
