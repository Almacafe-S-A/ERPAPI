using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FieldConciliacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConciliacionLinea_Conciliacion_ConciliacionId",
                table: "ConciliacionLinea");
               
            migrationBuilder.AlterColumn<int>(
                name: "ConciliacionId",
                table: "ConciliacionLinea",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConciliacionLinea_Conciliacion_ConciliacionId",
                table: "ConciliacionLinea",
                column: "ConciliacionId",
                principalTable: "Conciliacion",
                principalColumn: "ConciliacionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConciliacionLinea_Conciliacion_ConciliacionId",
                table: "ConciliacionLinea");

            migrationBuilder.AlterColumn<int>(
                name: "ConciliacionId",
                table: "ConciliacionLinea",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ConciliacionLinea_Conciliacion_ConciliacionId",
                table: "ConciliacionLinea",
                column: "ConciliacionId",
                principalTable: "Conciliacion",
                principalColumn: "ConciliacionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
