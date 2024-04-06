using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Option1 = table.Column<string>(type: "TEXT", nullable: false),
                    Option2 = table.Column<string>(type: "TEXT", nullable: false),
                    Option3 = table.Column<string>(type: "TEXT", nullable: false),
                    Option4 = table.Column<string>(type: "TEXT", nullable: false),
                    CorrectOption = table.Column<string>(type: "TEXT", nullable: false),
                    Explaination = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ChuDeId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoaiCauDeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
