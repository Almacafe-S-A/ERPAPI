using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SAldosFisicos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisico_Customer_CustomerId",
                table: "InventarioFisico");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "InventarioFisico",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisico_Customer_CustomerId",
                table: "InventarioFisico",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioFisico_Customer_CustomerId",
                table: "InventarioFisico");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "InventarioFisico",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioFisico_Customer_CustomerId",
                table: "InventarioFisico",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
