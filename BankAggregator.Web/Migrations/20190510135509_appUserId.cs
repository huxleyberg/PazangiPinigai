using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAggregator.Web.Migrations
{
    public partial class appUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountModels_AspNetUsers_UserID",
                table: "AccountModels");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "AccountModels",
                newName: "appUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountModels_UserID",
                table: "AccountModels",
                newName: "IX_AccountModels_appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountModels_AspNetUsers_appUserId",
                table: "AccountModels",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountModels_AspNetUsers_appUserId",
                table: "AccountModels");

            migrationBuilder.RenameColumn(
                name: "appUserId",
                table: "AccountModels",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_AccountModels_appUserId",
                table: "AccountModels",
                newName: "IX_AccountModels_UserID");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountModels_AspNetUsers_UserID",
                table: "AccountModels",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
