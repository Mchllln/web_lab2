using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Showtimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HallName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeatNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ShowtimeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Showtimes_ShowtimeId",
                        column: x => x.ShowtimeId,
                        principalTable: "Showtimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "Id", "HallName", "MovieTitle", "StartTime" },
                values: new object[,]
                {
                    { 1, "Hall A", "Inception", new DateTime(2026, 6, 1, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Hall B", "The Dark Knight", new DateTime(2026, 6, 1, 21, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Hall A", "Interstellar", new DateTime(2026, 6, 2, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Hall C", "Oppenheimer", new DateTime(2026, 6, 2, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Hall B", "Dune: Part Two", new DateTime(2026, 6, 3, 19, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CustomerName", "Price", "SeatNumber", "ShowtimeId" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", 8.50m, 12, 1 },
                    { 2, "Bob Smith", 10.00m, 7, 2 },
                    { 3, "Carol White", 8.50m, 24, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ShowtimeId",
                table: "Tickets",
                column: "ShowtimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Showtimes");
        }
    }
}
