using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FK_Serrvicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Propias",
                table: "InsurancePolicy",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitOfMeasureId",
                table: "ControlPallets",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_SubProductId",
                table: "InvoiceLine",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPallets_CustomerId",
                table: "ControlPallets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPallets_ProductId",
                table: "ControlPallets",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPallets_SubProductId",
                table: "ControlPallets",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPallets_UnitOfMeasureId",
                table: "ControlPallets",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoLine_SubProductId",
                table: "CertificadoLine",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDeposito_ServicioId",
                table: "CertificadoDeposito",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoDeposito_Product_ServicioId",
                table: "CertificadoDeposito",
                column: "ServicioId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoLine_SubProduct_SubProductId",
                table: "CertificadoLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_Customer_CustomerId",
                table: "ControlPallets",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_Product_ProductId",
                table: "ControlPallets",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_SubProduct_SubProductId",
                table: "ControlPallets",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlPallets_UnitOfMeasure_UnitOfMeasureId",
                table: "ControlPallets",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_SubProduct_SubProductId",
                table: "InvoiceLine",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoDeposito_Product_ServicioId",
                table: "CertificadoDeposito");

            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoLine_SubProduct_SubProductId",
                table: "CertificadoLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_Customer_CustomerId",
                table: "ControlPallets");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_Product_ProductId",
                table: "ControlPallets");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_SubProduct_SubProductId",
                table: "ControlPallets");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlPallets_UnitOfMeasure_UnitOfMeasureId",
                table: "ControlPallets");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_SubProduct_SubProductId",
                table: "InvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLine_SubProductId",
                table: "InvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_ControlPallets_CustomerId",
                table: "ControlPallets");

            migrationBuilder.DropIndex(
                name: "IX_ControlPallets_ProductId",
                table: "ControlPallets");

            migrationBuilder.DropIndex(
                name: "IX_ControlPallets_SubProductId",
                table: "ControlPallets");

            migrationBuilder.DropIndex(
                name: "IX_ControlPallets_UnitOfMeasureId",
                table: "ControlPallets");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoLine_SubProductId",
                table: "CertificadoLine");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoDeposito_ServicioId",
                table: "CertificadoDeposito");

            migrationBuilder.AlterColumn<bool>(
                name: "Propias",
                table: "InsurancePolicy",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<long>(
                name: "UnitOfMeasureId",
                table: "ControlPallets",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
