using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager_v2._0.Data.Migrations
{
    public partial class DesiredTotalAndCurrentTotalNamesInSavigsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "desiredTotal",
                table: "Savings",
                newName: "DesiredTotal");

            migrationBuilder.RenameColumn(
                name: "currentTotal",
                table: "Savings",
                newName: "CurrentTotal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DesiredTotal",
                table: "Savings",
                newName: "desiredTotal");

            migrationBuilder.RenameColumn(
                name: "CurrentTotal",
                table: "Savings",
                newName: "currentTotal");
        }
    }
}
