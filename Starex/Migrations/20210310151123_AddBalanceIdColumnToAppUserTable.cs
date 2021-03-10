using Microsoft.EntityFrameworkCore.Migrations;

namespace Starex.Migrations
{
    public partial class AddBalanceIdColumnToAppUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BalanceId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "Balance",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Price = table.Column<double>(nullable: false),
            //        Currency = table.Column<string>(nullable: false),
            //        MyBalance = table.Column<double>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Balance", x => x.Id);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BalanceId",
                table: "AspNetUsers",
                column: "BalanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Balances_BalanceId",
                table: "AspNetUsers",
                column: "BalanceId",
                principalTable: "Balances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Balances_BalanceId",
                table: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "Balance");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BalanceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BalanceId",
                table: "AspNetUsers");
        }
    }
}
