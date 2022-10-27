using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TypeQTYInventarioLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NSacos",
                table: "InventarioFisicoLines",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NSacos",
                table: "InventarioFisicoLines",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
