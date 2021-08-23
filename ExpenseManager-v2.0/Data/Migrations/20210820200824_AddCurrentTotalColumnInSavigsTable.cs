using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager_v2._0.Data.Migrations
{
    public partial class AddCurrentTotalColumnInSavigsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Savings",
                newName: "desiredTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "currentTotal",
                table: "Savings",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentTotal",
                table: "Savings");

            migrationBuilder.RenameColumn(
                name: "desiredTotal",
                table: "Savings",
                newName: "Total");
        }
    }
}
