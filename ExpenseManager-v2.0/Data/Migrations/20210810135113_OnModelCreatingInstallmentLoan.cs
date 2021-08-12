using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager_v2._0.Data.Migrations
{
    public partial class OnModelCreatingInstallmentLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentLoan_Credits_CreditId",
                table: "InstallmentLoan");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentLoan_Credits_CreditId",
                table: "InstallmentLoan",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentLoan_Credits_CreditId",
                table: "InstallmentLoan");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentLoan_Credits_CreditId",
                table: "InstallmentLoan",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
