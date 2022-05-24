using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course_API.Data.Migrations
{
    public partial class theCurrentDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
