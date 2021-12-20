using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class NombredetallereciboSolicitud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GoodsReceivedLineId",
                table: "SolicitudCertificadoLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoLine_GoodsReceivedLineId",
                table: "SolicitudCertificadoLine",
                column: "GoodsReceivedLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudCertificadoLine_GoodsReceivedLine_GoodsReceivedLineId",
                table: "SolicitudCertificadoLine",
                column: "GoodsReceivedLineId",
                principalTable: "GoodsReceivedLine",
                principalColumn: "GoodsReceiveLinedId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudCertificadoLine_GoodsReceivedLine_GoodsReceivedLineId",
                table: "SolicitudCertificadoLine");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudCertificadoLine_GoodsReceivedLineId",
                table: "SolicitudCertificadoLine");

            migrationBuilder.DropColumn(
                name: "GoodsReceivedLineId",
                table: "SolicitudCertificadoLine");
        }
    }
}
