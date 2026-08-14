using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDriveTypeToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarDriveType",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarDriveType",
                table: "Cars");
        }
    }
}
