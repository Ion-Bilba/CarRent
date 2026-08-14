using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarTraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarDriveType",
                table: "Cars",
                newName: "TractionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TractionType",
                table: "Cars",
                newName: "CarDriveType");
        }
    }
}
