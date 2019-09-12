using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ForeignsProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Grupos_GrupoId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Lineas_LineaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marcas_MarcaId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LineaId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GrupoId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Grupos_GrupoId",
                table: "Product",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "GrupoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Lineas_LineaId",
                table: "Product",
                column: "LineaId",
                principalTable: "Lineas",
                principalColumn: "LineaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Marcas_MarcaId",
                table: "Product",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Grupos_GrupoId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Lineas_LineaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marcas_MarcaId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LineaId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GrupoId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Grupos_GrupoId",
                table: "Product",
                column: "GrupoId",
                principalTable: "Grupos",
                principalColumn: "GrupoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Lineas_LineaId",
                table: "Product",
                column: "LineaId",
                principalTable: "Lineas",
                principalColumn: "LineaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Marcas_MarcaId",
                table: "Product",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
