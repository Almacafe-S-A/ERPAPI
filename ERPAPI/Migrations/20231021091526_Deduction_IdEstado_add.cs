using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Deduction_IdEstado_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deduction_Estados_EstadoId",
                table: "Deduction");

            migrationBuilder.AlterColumn<long>(
                name: "EstadoId",
                table: "Deduction",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Deduction",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Deduction_Estados_EstadoId",
                table: "Deduction",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deduction_Estados_EstadoId",
                table: "Deduction");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Deduction");

            migrationBuilder.AlterColumn<long>(
                name: "EstadoId",
                table: "Deduction",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deduction_Estados_EstadoId",
                table: "Deduction",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
