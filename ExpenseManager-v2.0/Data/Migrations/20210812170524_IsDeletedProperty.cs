using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager_v2._0.Data.Migrations
{
    public partial class IsDeletedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Credits_CreditId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Expenses_ExpenseId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Incomes_IncomeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreditId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ExpenseId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IncomeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreditId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IncomeId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InstallmentLoans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Incomes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Credits",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InstallmentLoans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Credits");

            migrationBuilder.AddColumn<int>(
                name: "CreditId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncomeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreditId",
                table: "AspNetUsers",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ExpenseId",
                table: "AspNetUsers",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IncomeId",
                table: "AspNetUsers",
                column: "IncomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Credits_CreditId",
                table: "AspNetUsers",
                column: "CreditId",
                principalTable: "Credits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Expenses_ExpenseId",
                table: "AspNetUsers",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Incomes_IncomeId",
                table: "AspNetUsers",
                column: "IncomeId",
                principalTable: "Incomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
