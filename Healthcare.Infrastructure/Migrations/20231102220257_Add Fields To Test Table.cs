using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToTestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Tests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tests");
        }
    }
}
