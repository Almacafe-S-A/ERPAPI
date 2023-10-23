using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Deduction_Fk_Estado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deduction_Estados_EstadoId",
                table: "Deduction");

            migrationBuilder.DropIndex(
                name: "IX_Deduction_EstadoId",
                table: "Deduction");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Deduction");

            migrationBuilder.CreateIndex(
                name: "IX_Deduction_IdEstado",
                table: "Deduction",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Deduction_Estados_IdEstado",
                table: "Deduction",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deduction_Estados_IdEstado",
                table: "Deduction");

            migrationBuilder.DropIndex(
                name: "IX_Deduction_IdEstado",
                table: "Deduction");

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "Deduction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deduction_EstadoId",
                table: "Deduction",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deduction_Estados_EstadoId",
                table: "Deduction",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
