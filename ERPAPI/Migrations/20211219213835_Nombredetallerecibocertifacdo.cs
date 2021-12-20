using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Nombredetallerecibocertifacdo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoLine_GoodsReceivedLine_GoodsReceivedId",
                table: "CertificadoLine");

            migrationBuilder.RenameColumn(
                name: "GoodsReceivedId",
                table: "CertificadoLine",
                newName: "GoodsReceivedLineId");

            migrationBuilder.RenameIndex(
                name: "IX_CertificadoLine_GoodsReceivedId",
                table: "CertificadoLine",
                newName: "IX_CertificadoLine_GoodsReceivedLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoLine_GoodsReceivedLine_GoodsReceivedLineId",
                table: "CertificadoLine",
                column: "GoodsReceivedLineId",
                principalTable: "GoodsReceivedLine",
                principalColumn: "GoodsReceiveLinedId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoLine_GoodsReceivedLine_GoodsReceivedLineId",
                table: "CertificadoLine");

            migrationBuilder.RenameColumn(
                name: "GoodsReceivedLineId",
                table: "CertificadoLine",
                newName: "GoodsReceivedId");

            migrationBuilder.RenameIndex(
                name: "IX_CertificadoLine_GoodsReceivedLineId",
                table: "CertificadoLine",
                newName: "IX_CertificadoLine_GoodsReceivedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoLine_GoodsReceivedLine_GoodsReceivedId",
                table: "CertificadoLine",
                column: "GoodsReceivedId",
                principalTable: "GoodsReceivedLine",
                principalColumn: "GoodsReceiveLinedId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
