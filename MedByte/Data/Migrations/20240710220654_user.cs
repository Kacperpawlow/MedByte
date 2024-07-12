using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedByte.Data.Migrations
{

    public partial class user : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tomografy",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tomografy");
        }
    }
}
