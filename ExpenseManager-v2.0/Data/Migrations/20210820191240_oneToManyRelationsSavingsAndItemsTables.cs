using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager_v2._0.Data.Migrations
{
    public partial class oneToManyRelationsSavingsAndItemsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedLibraries_AspNetUsers_UserId",
                table: "BorrowedLibraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Savings_AspNetUsers_UserId",
                table: "Savings");

            migrationBuilder.DropIndex(
                name: "IX_Savings_UserId",
                table: "Savings");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedLibraries_UserId",
                table: "BorrowedLibraries");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Savings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BorrowedLibraries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "BorrowedLibraries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Savings_UserId",
                table: "Savings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedLibraries_UserId",
                table: "BorrowedLibraries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedLibraries_AspNetUsers_UserId",
                table: "BorrowedLibraries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_AspNetUsers_UserId",
                table: "Savings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedLibraries_AspNetUsers_UserId",
                table: "BorrowedLibraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Savings_AspNetUsers_UserId",
                table: "Savings");

            migrationBuilder.DropIndex(
                name: "IX_Savings_UserId",
                table: "Savings");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedLibraries_UserId",
                table: "BorrowedLibraries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Savings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BorrowedLibraries");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "BorrowedLibraries");

            migrationBuilder.CreateIndex(
                name: "IX_Savings_UserId",
                table: "Savings",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedLibraries_UserId",
                table: "BorrowedLibraries",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedLibraries_AspNetUsers_UserId",
                table: "BorrowedLibraries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_AspNetUsers_UserId",
                table: "Savings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
