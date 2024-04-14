using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _130420244 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevel",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Questions");
        }
    }
}
