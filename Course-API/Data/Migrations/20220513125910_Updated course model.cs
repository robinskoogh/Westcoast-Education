using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course_API.Data.Migrations
{
    public partial class Updatedcoursemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "LengthUnit",
                table: "Courses",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthUnit",
                table: "Courses");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Length",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
