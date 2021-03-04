using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddCounrtyIdColumnToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CountryContacts_CountryId",
                table: "CountryContacts");

            migrationBuilder.DropIndex(
                name: "IX_BranchContacts_BranchId",
                table: "BranchContacts");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Addresses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CountryContacts_CountryId",
                table: "CountryContacts",
                column: "CountryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BranchContacts_BranchId",
                table: "BranchContacts",
                column: "BranchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_CountryContacts_CountryId",
                table: "CountryContacts");

            migrationBuilder.DropIndex(
                name: "IX_BranchContacts_BranchId",
                table: "BranchContacts");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CountryContacts_CountryId",
                table: "CountryContacts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchContacts_BranchId",
                table: "BranchContacts",
                column: "BranchId");
        }
    }
}
