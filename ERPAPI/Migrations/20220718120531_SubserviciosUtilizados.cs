using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SubserviciosUtilizados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad",
                table: "SubServicesWareHouse",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmpleadoId",
                table: "SubServicesWareHouse",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Solicitante",
                table: "SubServicesWareHouse",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubServicesWareHouse_EmpleadoId",
                table: "SubServicesWareHouse",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubServicesWareHouse_Employees_EmpleadoId",
                table: "SubServicesWareHouse",
                column: "EmpleadoId",
                principalTable: "Employees",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubServicesWareHouse_Employees_EmpleadoId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropIndex(
                name: "IX_SubServicesWareHouse_EmpleadoId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "SubServicesWareHouse");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "SubServicesWareHouse");

            migrationBuilder.DropColumn(
                name: "Solicitante",
                table: "SubServicesWareHouse");
        }
    }
}
