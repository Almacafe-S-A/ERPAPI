using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedStatusConciliacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "Conciliacion",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Conciliacion_EstadoId",
                table: "Conciliacion",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conciliacion_Estados_EstadoId",
                table: "Conciliacion",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conciliacion_Estados_EstadoId",
                table: "Conciliacion");

            migrationBuilder.DropIndex(
                name: "IX_Conciliacion_EstadoId",
                table: "Conciliacion");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Conciliacion");
        }
    }
}
