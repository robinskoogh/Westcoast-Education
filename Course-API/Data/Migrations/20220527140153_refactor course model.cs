using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course_API.Data.Migrations
{
    public partial class refactorcoursemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "LengthUnit",
                table: "Courses",
                newName: "Duration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Courses",
                newName: "LengthUnit");

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
