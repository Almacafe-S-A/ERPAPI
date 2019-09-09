using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ReciboMercaderia_Vigilante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VigilanteId",
                table: "GoodsReceived",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "VigilanteName",
                table: "GoodsReceived",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VigilanteId",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "VigilanteName",
                table: "GoodsReceived");
        }
    }
}
