using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class boletadasalidalineFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoletaDeSalidaLines_BoletaDeSalida_BoletaDeSalidaId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropIndex(
                name: "IX_BoletaDeSalidaLines_BoletaDeSalidaId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropColumn(
                name: "BoletaDeSalidaId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.AddColumn<long>(
                name: "BoletaSalidaId",
                table: "BoletaDeSalidaLines",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_BoletaDeSalidaLines_BoletaSalidaId",
                table: "BoletaDeSalidaLines",
                column: "BoletaSalidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoletaDeSalidaLines_BoletaDeSalida_BoletaSalidaId",
                table: "BoletaDeSalidaLines",
                column: "BoletaSalidaId",
                principalTable: "BoletaDeSalida",
                principalColumn: "BoletaDeSalidaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoletaDeSalidaLines_BoletaDeSalida_BoletaSalidaId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropIndex(
                name: "IX_BoletaDeSalidaLines_BoletaSalidaId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.DropColumn(
                name: "BoletaSalidaId",
                table: "BoletaDeSalidaLines");

            migrationBuilder.AddColumn<long>(
                name: "BoletaDeSalidaId",
                table: "BoletaDeSalidaLines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoletaDeSalidaLines_BoletaDeSalidaId",
                table: "BoletaDeSalidaLines",
                column: "BoletaDeSalidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoletaDeSalidaLines_BoletaDeSalida_BoletaDeSalidaId",
                table: "BoletaDeSalidaLines",
                column: "BoletaDeSalidaId",
                principalTable: "BoletaDeSalida",
                principalColumn: "BoletaDeSalidaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
