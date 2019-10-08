using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerNuevosCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsExonerado",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SolicitadoPor",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsExonerado",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "SolicitadoPor",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Customer");
        }
    }
}
