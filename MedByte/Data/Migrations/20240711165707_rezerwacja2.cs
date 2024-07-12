using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedByte.Data.Migrations
{

    public partial class rezerwacja2 : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imie",
                table: "Rezerwacje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nazwisko",
                table: "Rezerwacje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pesel",
                table: "Rezerwacje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imie",
                table: "Rezerwacje");

            migrationBuilder.DropColumn(
                name: "Nazwisko",
                table: "Rezerwacje");

            migrationBuilder.DropColumn(
                name: "Pesel",
                table: "Rezerwacje");
        }
    }
}
