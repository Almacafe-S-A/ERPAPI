using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ModificacionHorasExtraTarde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "LlegadasTardeBiometrico",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "HorasExtrasBiometrico",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_LlegadasTardeBiometrico_IdEstado",
                table: "LlegadasTardeBiometrico",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_HorasExtrasBiometrico_IdEstado",
                table: "HorasExtrasBiometrico",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_HorasExtrasBiometrico_Estados_IdEstado",
                table: "HorasExtrasBiometrico",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LlegadasTardeBiometrico_Estados_IdEstado",
                table: "LlegadasTardeBiometrico",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorasExtrasBiometrico_Estados_IdEstado",
                table: "HorasExtrasBiometrico");

            migrationBuilder.DropForeignKey(
                name: "FK_LlegadasTardeBiometrico_Estados_IdEstado",
                table: "LlegadasTardeBiometrico");

            migrationBuilder.DropIndex(
                name: "IX_LlegadasTardeBiometrico_IdEstado",
                table: "LlegadasTardeBiometrico");

            migrationBuilder.DropIndex(
                name: "IX_HorasExtrasBiometrico_IdEstado",
                table: "HorasExtrasBiometrico");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "LlegadasTardeBiometrico");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "HorasExtrasBiometrico");
        }
    }
}
