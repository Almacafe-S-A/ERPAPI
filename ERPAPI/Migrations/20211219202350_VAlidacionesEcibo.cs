using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class VAlidacionesEcibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "esCafe",
                table: "GoodsReceived",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GoodsReceivedId",
                table: "CertificadoLine",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoLine_GoodsReceivedId",
                table: "CertificadoLine",
                column: "GoodsReceivedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoLine_GoodsReceivedLine_GoodsReceivedId",
                table: "CertificadoLine",
                column: "GoodsReceivedId",
                principalTable: "GoodsReceivedLine",
                principalColumn: "GoodsReceiveLinedId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoLine_GoodsReceivedLine_GoodsReceivedId",
                table: "CertificadoLine");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoLine_GoodsReceivedId",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "esCafe",
                table: "GoodsReceived");

            migrationBuilder.DropColumn(
                name: "GoodsReceivedId",
                table: "CertificadoLine");
        }
    }
}
