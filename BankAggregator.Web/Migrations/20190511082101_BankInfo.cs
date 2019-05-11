using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAggregator.Web.Migrations
{
    public partial class BankInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "AccountModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountName",
                table: "AccountModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SandboxIdentification",
                table: "AccountModels",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExpense",
                table: "AccountModels",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalIncome",
                table: "AccountModels",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "AccountModels");

            migrationBuilder.DropColumn(
                name: "BankAccountName",
                table: "AccountModels");

            migrationBuilder.DropColumn(
                name: "SandboxIdentification",
                table: "AccountModels");

            migrationBuilder.DropColumn(
                name: "TotalExpense",
                table: "AccountModels");

            migrationBuilder.DropColumn(
                name: "TotalIncome",
                table: "AccountModels");
        }
    }
}
