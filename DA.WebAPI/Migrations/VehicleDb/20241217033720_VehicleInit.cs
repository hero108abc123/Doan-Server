﻿using System;
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
            migrationBuilder.EnsureSchema(
                name: "vehicle");

            migrationBuilder.CreateTable(
                name: "VehicleBus",
                schema: "vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSeat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleBusRide",
                schema: "vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RideName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBusRide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleBusRide_VehicleBus_BusId",
                        column: x => x.BusId,
                        principalSchema: "vehicle",
                        principalTable: "VehicleBus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleSeat",
                schema: "vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSeat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleSeat_VehicleBusRide_BusId",
                        column: x => x.BusId,
                        principalSchema: "vehicle",
                        principalTable: "VehicleBusRide",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleBusRide_BusId",
                schema: "vehicle",
                table: "VehicleBusRide",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSeat_BusId",
                schema: "vehicle",
                table: "VehicleSeat",
                column: "BusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleSeat",
                schema: "vehicle");

            migrationBuilder.DropTable(
                name: "VehicleBusRide",
                schema: "vehicle");

            migrationBuilder.DropTable(
                name: "VehicleBus",
                schema: "vehicle");
        }
    }
}
