using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ownership.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialOwnershipMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ownership");

            migrationBuilder.CreateTable(
                name: "Owners",
                schema: "Ownership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerUnits",
                schema: "Ownership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerUnits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerUnits_UnitId",
                schema: "Ownership",
                table: "OwnerUnits",
                column: "UnitId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Owners",
                schema: "Ownership");

            migrationBuilder.DropTable(
                name: "OwnerUnits",
                schema: "Ownership");
        }
    }
}
