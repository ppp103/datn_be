using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _140420248 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Topics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Topics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Topics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Topics",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Topics");
        }
    }
}
