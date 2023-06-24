using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class GetDateFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover_name",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AccountCreated",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<string>(
                name: "CoverPath",
                table: "Books",
                type: "TEXT",
                nullable: true,
                defaultValue: "default.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPath",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AccountCreated",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Cover_name",
                table: "Books",
                type: "TEXT",
                nullable: true);
        }
    }
}
