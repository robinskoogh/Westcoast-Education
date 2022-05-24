using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course_API.Data.Migrations
{
    public partial class Changedaddressofperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Students",
                newName: "StreetAddress");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Students",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Students",
                newName: "Address");
        }
    }
}
