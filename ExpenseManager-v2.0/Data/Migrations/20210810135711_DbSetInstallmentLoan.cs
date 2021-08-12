using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager_v2._0.Data.Migrations
{
    public partial class DbSetInstallmentLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentLoan_Credits_CreditId",
                table: "InstallmentLoan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallmentLoan",
                table: "InstallmentLoan");

            migrationBuilder.RenameTable(
                name: "InstallmentLoan",
                newName: "InstallmentLoans");

            migrationBuilder.RenameIndex(
                name: "IX_InstallmentLoan_CreditId",
                table: "InstallmentLoans",
                newName: "IX_InstallmentLoans_CreditId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallmentLoans",
                table: "InstallmentLoans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentLoans_Credits_CreditId",
                table: "InstallmentLoans",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallmentLoans_Credits_CreditId",
                table: "InstallmentLoans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallmentLoans",
                table: "InstallmentLoans");

            migrationBuilder.RenameTable(
                name: "InstallmentLoans",
                newName: "InstallmentLoan");

            migrationBuilder.RenameIndex(
                name: "IX_InstallmentLoans_CreditId",
                table: "InstallmentLoan",
                newName: "IX_InstallmentLoan_CreditId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallmentLoan",
                table: "InstallmentLoan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallmentLoan_Credits_CreditId",
                table: "InstallmentLoan",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
