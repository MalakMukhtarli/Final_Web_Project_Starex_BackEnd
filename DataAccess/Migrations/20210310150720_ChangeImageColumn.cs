using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ChangeImageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "PhotoHeader",
                table: "Bios");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoFooterImage",
                table: "Bios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoHeaderImage",
                table: "Bios",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BalanceId",
                table: "AppUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_BalanceId",
                table: "AppUser",
                column: "BalanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUser_Balances_BalanceId",
                table: "AppUser",
                column: "BalanceId",
                principalTable: "Balances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUser_Balances_BalanceId",
                table: "AppUser");

            migrationBuilder.DropIndex(
                name: "IX_AppUser_BalanceId",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "LogoFooterImage",
                table: "Bios");

            migrationBuilder.DropColumn(
                name: "LogoHeaderImage",
                table: "Bios");

            migrationBuilder.DropColumn(
                name: "BalanceId",
                table: "AppUser");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoHeader",
                table: "Bios",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
