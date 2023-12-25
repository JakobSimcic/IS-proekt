using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bojan_recipe.Data.Migrations
{
    public partial class PodaljsekRecepta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Recipe",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_OwnerId",
                table: "Recipe",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_AspNetUsers_OwnerId",
                table: "Recipe",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_AspNetUsers_OwnerId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_OwnerId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "AspNetUsers");
        }
    }
}
