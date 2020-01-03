using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CorrecionFK_TipoDocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_TiposDocumento_EstadoId",
                table: "InventoryTransfer");

            migrationBuilder.AlterColumn<long>(
                name: "TipoDocumentoId",
                table: "InventoryTransfer",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_TipoDocumentoId",
                table: "InventoryTransfer",
                column: "TipoDocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_TiposDocumento_TipoDocumentoId",
                table: "InventoryTransfer",
                column: "TipoDocumentoId",
                principalTable: "TiposDocumento",
                principalColumn: "IdTipoDocumento",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_TiposDocumento_TipoDocumentoId",
                table: "InventoryTransfer");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_TipoDocumentoId",
                table: "InventoryTransfer");

            migrationBuilder.AlterColumn<int>(
                name: "TipoDocumentoId",
                table: "InventoryTransfer",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_TiposDocumento_EstadoId",
                table: "InventoryTransfer",
                column: "EstadoId",
                principalTable: "TiposDocumento",
                principalColumn: "IdTipoDocumento",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
