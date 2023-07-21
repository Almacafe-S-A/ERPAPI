using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TBL_ADD_InsuranceCertificate_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ServicioId",
                table: "InsuranceCertificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicioName",
                table: "InsuranceCertificate",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCertificate_ServicioId",
                table: "InsuranceCertificate",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceCertificate_Product_ServicioId",
                table: "InsuranceCertificate",
                column: "ServicioId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceCertificate_Product_ServicioId",
                table: "InsuranceCertificate");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceCertificate_ServicioId",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "ServicioId",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "ServicioName",
                table: "InsuranceCertificate");
        }
    }
}
