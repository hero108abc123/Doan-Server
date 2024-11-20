using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DA.WebAPI.Migrations.VehicleDb
{
    /// <inheritdoc />
    public partial class VehicleInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isAvailable",
                schema: "vehicle",
                table: "VehicleSeat",
                newName: "IsAvailable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                schema: "vehicle",
                table: "VehicleSeat",
                newName: "isAvailable");
        }
    }
}
