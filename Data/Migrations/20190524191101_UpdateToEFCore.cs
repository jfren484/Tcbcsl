using Microsoft.EntityFrameworkCore.Migrations;

namespace Tcbcsl.Data.Migrations
{
    public partial class UpdateToEFCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo.TcbcslUserTeams_dbo.AspNetUsers_TcbcslUser_Id",
                table: "TcbcslUserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo.TcbcslUserTeams_dbo.Teams_Team_TeamId",
                table: "TcbcslUserTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo.TcbcslUserTeams",
                table: "TcbcslUserTeams");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFinalized",
                table: "Games",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.RenameIndex(
                name: "IX_Team_TeamId",
                table: "TcbcslUserTeams",
                newName: "IX_TcbcslUserTeams_TeamId");

            migrationBuilder.RenameColumn(
                name: "Team_TeamId",
                table: "TcbcslUserTeams",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "TcbcslUser_Id",
                table: "TcbcslUserTeams",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TcbcslUserTeams",
                table: "TcbcslUserTeams",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TcbcslUserTeams_Teams_TeamId",
                table: "TcbcslUserTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TcbcslUserTeams_AspNetUsers_UserId",
                table: "TcbcslUserTeams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(name: "PK_dbo.Addresses", table: "Addresses", newName: "PK_Addresses");
            migrationBuilder.RenameIndex(name: "PK_dbo.AspNetRoles", table: "AspNetRoles", newName: "PK_AspNetRoles");
            migrationBuilder.RenameIndex(name: "PK_dbo.AspNetUserClaims", table: "AspNetUserClaims", newName: "PK_AspNetUserClaims");
            migrationBuilder.RenameIndex(name: "PK_dbo.AspNetUserLogins", table: "AspNetUserLogins", newName: "PK_AspNetUserLogins");
            migrationBuilder.RenameIndex(name: "PK_dbo.AspNetUserRoles", table: "AspNetUserRoles", newName: "PK_AspNetUserRoles");
            migrationBuilder.RenameIndex(name: "PK_dbo.AspNetUsers", table: "AspNetUsers", newName: "PK_AspNetUsers");
            migrationBuilder.RenameIndex(name: "PK_dbo.Churches", table: "Churches", newName: "PK_Churches");
            migrationBuilder.RenameIndex(name: "PK_dbo.Coaches", table: "Coaches", newName: "PK_Coaches");
            migrationBuilder.RenameIndex(name: "PK_dbo.Conferences", table: "Conferences", newName: "PK_Conferences");
            migrationBuilder.RenameIndex(name: "PK_dbo.ConferenceYears", table: "ConferenceYears", newName: "PK_ConferenceYears");
            migrationBuilder.RenameIndex(name: "PK_dbo.ContactPhoneNumbers", table: "ContactPhoneNumbers", newName: "PK_ContactPhoneNumbers");
            migrationBuilder.RenameIndex(name: "PK_dbo.Divisions", table: "Divisions", newName: "PK_Divisions");
            migrationBuilder.RenameIndex(name: "PK_dbo.DivisionYears", table: "DivisionYears", newName: "PK_DivisionYears");
            migrationBuilder.RenameIndex(name: "PK_dbo.GameParticipants", table: "GameParticipants", newName: "PK_GameParticipants");
            migrationBuilder.RenameIndex(name: "PK_dbo.GameResultReports", table: "GameResultReports", newName: "PK_GameResultReports");
            migrationBuilder.RenameIndex(name: "PK_dbo.Games", table: "Games", newName: "PK_Games");
            migrationBuilder.RenameIndex(name: "PK_dbo.GameStatus", table: "GameStatus", newName: "PK_GameStatus");
            migrationBuilder.RenameIndex(name: "PK_dbo.GameTournamentDates", table: "GameTournamentDates", newName: "PK_GameTournamentDates");
            migrationBuilder.RenameIndex(name: "PK_dbo.GameTypes", table: "GameTypes", newName: "PK_GameTypes");
            migrationBuilder.RenameIndex(name: "PK_dbo.NewsItems", table: "NewsItems", newName: "PK_NewsItems");
            migrationBuilder.RenameIndex(name: "PK_dbo.PageContents", table: "PageContents", newName: "PK_PageContents");
            migrationBuilder.RenameIndex(name: "PK_dbo.PhoneNumberTypes", table: "PhoneNumberTypes", newName: "PK_PhoneNumberTypes");
            migrationBuilder.RenameIndex(name: "PK_dbo.Players", table: "Players", newName: "PK_Players");
            migrationBuilder.RenameIndex(name: "PK_dbo.States", table: "States", newName: "PK_States");
            migrationBuilder.RenameIndex(name: "PK_dbo.StatLines", table: "StatLines", newName: "PK_StatLines");
            migrationBuilder.RenameIndex(name: "PK_dbo.Teams", table: "Teams", newName: "PK_Teams");
            migrationBuilder.RenameIndex(name: "PK_dbo.TeamYears", table: "TeamYears", newName: "PK_TeamYears");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(name: "PK_Addresses", table: "Addresses", newName: "PK_dbo.Addresses");
            migrationBuilder.RenameIndex(name: "PK_AspNetRoles", table: "AspNetRoles", newName: "PK_dbo.AspNetRoles");
            migrationBuilder.RenameIndex(name: "PK_AspNetUserClaims", table: "AspNetUserClaims", newName: "PK_dbo.AspNetUserClaims");
            migrationBuilder.RenameIndex(name: "PK_AspNetUserLogins", table: "AspNetUserLogins", newName: "PK_dbo.AspNetUserLogins");
            migrationBuilder.RenameIndex(name: "PK_AspNetUserRoles", table: "AspNetUserRoles", newName: "PK_dbo.AspNetUserRoles");
            migrationBuilder.RenameIndex(name: "PK_AspNetUsers", table: "AspNetUsers", newName: "PK_dbo.AspNetUsers");
            migrationBuilder.RenameIndex(name: "PK_Churches", table: "Churches", newName: "PK_dbo.Churches");
            migrationBuilder.RenameIndex(name: "PK_Coaches", table: "Coaches", newName: "PK_dbo.Coaches");
            migrationBuilder.RenameIndex(name: "PK_Conferences", table: "Conferences", newName: "PK_dbo.Conferences");
            migrationBuilder.RenameIndex(name: "PK_ConferenceYears", table: "ConferenceYears", newName: "PK_dbo.ConferenceYears");
            migrationBuilder.RenameIndex(name: "PK_ContactPhoneNumbers", table: "ContactPhoneNumbers", newName: "PK_dbo.ContactPhoneNumbers");
            migrationBuilder.RenameIndex(name: "PK_Divisions", table: "Divisions", newName: "PK_dbo.Divisions");
            migrationBuilder.RenameIndex(name: "PK_DivisionYears", table: "DivisionYears", newName: "PK_dbo.DivisionYears");
            migrationBuilder.RenameIndex(name: "PK_GameParticipants", table: "GameParticipants", newName: "PK_dbo.GameParticipants");
            migrationBuilder.RenameIndex(name: "PK_GameResultReports", table: "GameResultReports", newName: "PK_dbo.GameResultReports");
            migrationBuilder.RenameIndex(name: "PK_Games", table: "Games", newName: "PK_dbo.Games");
            migrationBuilder.RenameIndex(name: "PK_GameStatus", table: "GameStatus", newName: "PK_dbo.GameStatus");
            migrationBuilder.RenameIndex(name: "PK_GameTournamentDates", table: "GameTournamentDates", newName: "PK_dbo.GameTournamentDates");
            migrationBuilder.RenameIndex(name: "PK_GameTypes", table: "GameTypes", newName: "PK_dbo.GameTypes");
            migrationBuilder.RenameIndex(name: "PK_NewsItems", table: "NewsItems", newName: "PK_dbo.NewsItems");
            migrationBuilder.RenameIndex(name: "PK_PageContents", table: "PageContents", newName: "PK_dbo.PageContents");
            migrationBuilder.RenameIndex(name: "PK_PhoneNumberTypes", table: "PhoneNumberTypes", newName: "PK_dbo.PhoneNumberTypes");
            migrationBuilder.RenameIndex(name: "PK_Players", table: "Players", newName: "PK_dbo.Players");
            migrationBuilder.RenameIndex(name: "PK_States", table: "States", newName: "PK_dbo.States");
            migrationBuilder.RenameIndex(name: "PK_StatLines", table: "StatLines", newName: "PK_dbo.StatLines");
            migrationBuilder.RenameIndex(name: "PK_Teams", table: "Teams", newName: "PK_dbo.Teams");
            migrationBuilder.RenameIndex(name: "PK_TeamYears", table: "TeamYears", newName: "PK_dbo.TeamYears");

            migrationBuilder.DropForeignKey(
                name: "FK_TcbcslUserTeams_Teams_TeamId",
                table: "TcbcslUserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_TcbcslUserTeams_AspNetUsers_UserId",
                table: "TcbcslUserTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TcbcslUserTeams",
                table: "TcbcslUserTeams");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TcbcslUserTeams",
                newName: "Team_TeamId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TcbcslUserTeams",
                newName: "TcbcslUser_Id");

            migrationBuilder.RenameIndex(
                name: "IX_TcbcslUserTeams_TeamId",
                table: "TcbcslUserTeams",
                newName: "IX_Team_TeamId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFinalized",
                table: "Games",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo.TcbcslUserTeams",
                table: "TcbcslUserTeams",
                columns: new[] { "TcbcslUser_Id", "Team_TeamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.TcbcslUserTeams_dbo.Teams_Team_TeamId",
                table: "TcbcslUserTeams",
                column: "Team_TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo.TcbcslUserTeams_dbo.AspNetUsers_TcbcslUser_Id",
                table: "TcbcslUserTeams",
                column: "TcbcslUser_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
