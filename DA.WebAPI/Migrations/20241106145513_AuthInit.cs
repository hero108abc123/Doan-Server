using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DA.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AuthInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fullname",
                schema: "auth",
                table: "AuthCustomer",
                newName: "FullName");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "auth",
                table: "AuthCustomer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AuthCustomer_UserId",
                schema: "auth",
                table: "AuthCustomer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthCustomer_AuthUser_UserId",
                schema: "auth",
                table: "AuthCustomer",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "AuthUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthCustomer_AuthUser_UserId",
                schema: "auth",
                table: "AuthCustomer");

            migrationBuilder.DropIndex(
                name: "IX_AuthCustomer_UserId",
                schema: "auth",
                table: "AuthCustomer");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "auth",
                table: "AuthCustomer");

            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "auth",
                table: "AuthCustomer",
                newName: "Fullname");
        }
    }
}
