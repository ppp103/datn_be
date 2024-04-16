using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _160420242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPoint",
                table: "Tests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPoint",
                table: "Tests");
        }
    }
}
