using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RelacionEmpleadoEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Estados_IdEstado",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Estados_IdEstado",
                table: "Employees",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Estados_IdEstado",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Estados_IdEstado",
                table: "Employees",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
