using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SaldosCertificarenRecibos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SaldoporCertificar",
                table: "GoodsReceivedLine",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Porcertificar",
                table: "GoodsReceived",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaldoporCertificar",
                table: "GoodsReceivedLine");

            migrationBuilder.DropColumn(
                name: "Porcertificar",
                table: "GoodsReceived");
        }
    }
}
