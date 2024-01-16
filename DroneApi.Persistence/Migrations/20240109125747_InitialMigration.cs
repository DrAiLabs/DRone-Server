using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DroneApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubmitedOrders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessLocation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DropoffLocation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitedOrders", x => x.OrderId);
                });

            migrationBuilder.InsertData(
                table: "SubmitedOrders",
                columns: new[] { "OrderId", "BusinessLocation", "DropoffLocation" },
                values: new object[] { new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"), "Villa Mella-Sector14", "dropoff-station-4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmitedOrders");
        }
    }
}
