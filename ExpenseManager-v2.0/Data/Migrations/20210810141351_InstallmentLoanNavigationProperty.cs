using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager_v2._0.Data.Migrations
{
    public partial class InstallmentLoanNavigationProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditId1",
                table: "InstallmentLoans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentLoans_CreditId1",
                table: "InstallmentLoans",
                column: "CreditId1",
                unique: true,
                filter: "[CreditId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentLoans_Credits_CreditId1",
                table: "InstallmentLoans",
                column: "CreditId1",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentLoans_Credits_CreditId1",
                table: "InstallmentLoans");

            migrationBuilder.DropIndex(
                name: "IX_InstallmentLoans_CreditId1",
                table: "InstallmentLoans");

            migrationBuilder.DropColumn(
                name: "CreditId1",
                table: "InstallmentLoans");
        }
    }
}
