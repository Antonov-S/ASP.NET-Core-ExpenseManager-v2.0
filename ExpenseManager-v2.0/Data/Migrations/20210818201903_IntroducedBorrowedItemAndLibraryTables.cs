using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseManager_v2._0.Data.Migrations
{
    public partial class IntroducedBorrowedItemAndLibraryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BorrowedLibraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedLibraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowedLibraries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    BorrowedLibraryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowedItems_BorrowedLibraries_BorrowedLibraryId",
                        column: x => x.BorrowedLibraryId,
                        principalTable: "BorrowedLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedItems_BorrowedLibraryId",
                table: "BorrowedItems",
                column: "BorrowedLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedLibraries_UserId",
                table: "BorrowedLibraries",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowedItems");

            migrationBuilder.DropTable(
                name: "BorrowedLibraries");
        }
    }
}
