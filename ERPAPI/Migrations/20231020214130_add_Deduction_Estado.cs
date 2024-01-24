using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class add_Deduction_Estado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Feriados",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "Deduction",
                nullable: true,
                defaultValue: 0L);

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Feriados",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
