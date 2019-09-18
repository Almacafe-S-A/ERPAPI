using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PEPS_Country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Observacion",
                table: "PEPS",
                newName: "Periodo");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "PEPS",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PartidoPoliticoId",
                table: "PEPS",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PartidoPoliticoName",
                table: "PEPS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "PEPS");

            migrationBuilder.DropColumn(
                name: "PartidoPoliticoId",
                table: "PEPS");

            migrationBuilder.DropColumn(
                name: "PartidoPoliticoName",
                table: "PEPS");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Country");

            migrationBuilder.RenameColumn(
                name: "Periodo",
                table: "PEPS",
                newName: "Observacion");
        }
    }
}
