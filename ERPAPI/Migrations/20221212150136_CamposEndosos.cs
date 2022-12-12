using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CamposEndosos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadEndosada",
                table: "EndososCertificados");

            migrationBuilder.DropColumn(
                name: "TipoEndosoId",
                table: "EndososCertificados");

            migrationBuilder.DropColumn(
                name: "TipoEndosoName",
                table: "EndososCertificados");

            migrationBuilder.DropColumn(
                name: "TotalCantidad",
                table: "EndososCertificados");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CantidadEndosada",
                table: "EndososCertificados",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "TipoEndosoId",
                table: "EndososCertificados",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TipoEndosoName",
                table: "EndososCertificados",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCantidad",
                table: "EndososCertificados",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
