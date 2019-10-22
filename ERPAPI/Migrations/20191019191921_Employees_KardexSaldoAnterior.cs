using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Employees_KardexSaldoAnterior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SaldoAnterior",
                table: "KardexLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalary_IdEmpleado",
                table: "EmployeeSalary",
                column: "IdEmpleado");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalary_Employees_IdEmpleado",
                table: "EmployeeSalary",
                column: "IdEmpleado",
                principalTable: "Employees",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalary_Employees_IdEmpleado",
                table: "EmployeeSalary");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalary_IdEmpleado",
                table: "EmployeeSalary");

            migrationBuilder.DropColumn(
                name: "SaldoAnterior",
                table: "KardexLine");
        }
    }
}
