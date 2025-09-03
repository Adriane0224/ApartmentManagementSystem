using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitOwnerView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitOwnerView",
                schema: "Apartment",
                columns: table => new
                {
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOwnerView", x => x.UnitId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitOwnerView",
                schema: "Apartment");
        }
    }
}
