using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddNewsIdToNewsDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "NewsDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NewsDetail_NewsId",
                table: "NewsDetail",
                column: "NewsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsDetail_News_NewsId",
                table: "NewsDetail",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsDetail_News_NewsId",
                table: "NewsDetail");

            migrationBuilder.DropIndex(
                name: "IX_NewsDetail_NewsId",
                table: "NewsDetail");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "NewsDetail");
        }
    }
}
