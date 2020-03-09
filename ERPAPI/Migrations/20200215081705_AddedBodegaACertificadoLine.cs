using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedBodegaACertificadoLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WarehouseId",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "CertificadoLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "CertificadoLine");
        }
    }
}
