using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldControlAsistenciaIdEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlAsistencias_Employees_IdEmpleado",
                table: "ControlAsistencias");

            migrationBuilder.AlterColumn<long>(
                name: "IdEmpleado",
                table: "ControlAsistencias",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlAsistencias_Employees_IdEmpleado",
                table: "ControlAsistencias",
                column: "IdEmpleado",
                principalTable: "Employees",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlAsistencias_Employees_IdEmpleado",
                table: "ControlAsistencias");

            migrationBuilder.AlterColumn<long>(
                name: "IdEmpleado",
                table: "ControlAsistencias",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_ControlAsistencias_Employees_IdEmpleado",
                table: "ControlAsistencias",
                column: "IdEmpleado",
                principalTable: "Employees",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
