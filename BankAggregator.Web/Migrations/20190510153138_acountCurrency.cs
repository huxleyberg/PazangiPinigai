using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAggregator.Web.Migrations
{
    public partial class acountCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "AccountModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "AccountModels");
        }
    }
}
