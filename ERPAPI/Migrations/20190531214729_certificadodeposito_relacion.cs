using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class certificadodeposito_relacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificadoId",
                table: "CertificadoLine");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "CertificadoLine",
                newName: "SubProductName");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CertificadoLine",
                newName: "SubProductId");

            migrationBuilder.AddColumn<long>(
                name: "IdCD",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ServicioName",
                table: "CertificadoDeposito",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "CertificadoDeposito",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoLine_IdCD",
                table: "CertificadoLine",
                column: "IdCD");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoLine_CertificadoDeposito_IdCD",
                table: "CertificadoLine",
                column: "IdCD",
                principalTable: "CertificadoDeposito",
                principalColumn: "IdCD",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoLine_CertificadoDeposito_IdCD",
                table: "CertificadoLine");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoLine_IdCD",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "IdCD",
                table: "CertificadoLine");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "ServicioName",
                table: "CertificadoDeposito");

            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "CertificadoDeposito");

            migrationBuilder.RenameColumn(
                name: "SubProductName",
                table: "CertificadoLine",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "SubProductId",
                table: "CertificadoLine",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "CertificadoId",
                table: "CertificadoLine",
                nullable: false,
                defaultValue: 0);
        }
    }
}
