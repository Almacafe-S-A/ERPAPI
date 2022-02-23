using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BranchKeyWarehose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Warehouse",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_BranchId",
                table: "Warehouse",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Branch_BranchId",
                table: "Warehouse",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Branch_BranchId",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_BranchId",
                table: "Warehouse");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Warehouse",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
