using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Tcbcsl.Data.Migrations
{
    public partial class UpdateIdentityToCore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "AspNetRoles",
                maxLength: 256,
                nullable: true);

            migrationBuilder.Sql("UPDATE AspNetRoles SET NormalizedName = LOWER(Name)");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<bool>(
                name: "UserName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.Sql("UPDATE AspNetUsers SET NormalizedUserName = LOWER(UserName)");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.Sql("UPDATE AspNetUsers SET NormalizedEmail = LOWER(Email)");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "LockoutEndDateUtc",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(maxLength: 128, nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<string>(
                name: "ProviderDisplayName",
                table: "AspNetUserLogins",
                nullable: true);

            migrationBuilder.Sql("UPDATE AspNetUserLogins SET ProviderDisplayName = LoginProvider");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 128, nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFinalized",
                table: "Games",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsFinalized",
                table: "Games",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropTable(name: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ProviderDisplayName",
                table: "AspNetUserLogins");

            migrationBuilder.DropTable(name: "AspNetRoleClaims");

            migrationBuilder.AddColumn<DateTime>(
                name: "LockoutEndDateUtc",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<bool>(
                name: "UserName",
                table: "AspNetUsers",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "AspNetRoles");
        }
    }
}
