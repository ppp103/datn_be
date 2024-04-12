using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _120420242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChuDe",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "LoaiCau",
                table: "Questions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChuDe",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LoaiCau",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
