using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AgregadoCampoEstado_Insurances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Insurances", true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Insurances",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "Insurances",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_EstadoId",
                table: "Insurances",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Estados_EstadoId",
                table: "Insurances",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Estados_EstadoId",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_EstadoId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Insurances");
        }
    }
}
