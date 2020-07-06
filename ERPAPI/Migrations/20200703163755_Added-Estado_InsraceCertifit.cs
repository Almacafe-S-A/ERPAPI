using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedEstado_InsraceCertifit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "InsuranceCertificate",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCertificate_EstadoId",
                table: "InsuranceCertificate",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceCertificate_Estados_EstadoId",
                table: "InsuranceCertificate",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceCertificate_Estados_EstadoId",
                table: "InsuranceCertificate");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceCertificate_EstadoId",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "InsuranceCertificate");
        }
    }
}
