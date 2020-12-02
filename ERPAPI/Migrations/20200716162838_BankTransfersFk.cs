using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BankTransfersFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountTransfers_Estados_EstadosIdEstado",
                table: "BankAccountTransfers");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountTransfers_EstadosIdEstado",
                table: "BankAccountTransfers");

            migrationBuilder.DropColumn(
                name: "EstadosIdEstado",
                table: "BankAccountTransfers");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransfers_EstadoId",
                table: "BankAccountTransfers",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountTransfers_Estados_EstadoId",
                table: "BankAccountTransfers",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccountTransfers_Estados_EstadoId",
                table: "BankAccountTransfers");

            migrationBuilder.DropIndex(
                name: "IX_BankAccountTransfers_EstadoId",
                table: "BankAccountTransfers");

            migrationBuilder.AddColumn<long>(
                name: "EstadosIdEstado",
                table: "BankAccountTransfers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransfers_EstadosIdEstado",
                table: "BankAccountTransfers",
                column: "EstadosIdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccountTransfers_Estados_EstadosIdEstado",
                table: "BankAccountTransfers",
                column: "EstadosIdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
