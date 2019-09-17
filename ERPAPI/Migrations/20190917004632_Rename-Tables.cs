using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Lineas_LineaId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Grupos_GrupoId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Lineas_LineaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marcas_MarcaId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marcas",
                table: "Marcas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lineas",
                table: "Lineas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grupos",
                table: "Grupos");

            migrationBuilder.RenameTable(
                name: "Marcas",
                newName: "Marca");

            migrationBuilder.RenameTable(
                name: "Lineas",
                newName: "Linea");

            migrationBuilder.RenameTable(
                name: "Grupos",
                newName: "Grupo");

            migrationBuilder.RenameIndex(
                name: "IX_Grupos_LineaId",
                table: "Grupo",
                newName: "IX_Grupo_LineaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marca",
                table: "Marca",
                column: "MarcaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Linea",
                table: "Linea",
                column: "LineaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grupo",
                table: "Grupo",
                column: "GrupoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupo_Linea_LineaId",
                table: "Grupo",
                column: "LineaId",
                principalTable: "Linea",
                principalColumn: "LineaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Grupo_GrupoId",
                table: "Product",
                column: "GrupoId",
                principalTable: "Grupo",
                principalColumn: "GrupoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Linea_LineaId",
                table: "Product",
                column: "LineaId",
                principalTable: "Linea",
                principalColumn: "LineaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Marca_MarcaId",
                table: "Product",
                column: "MarcaId",
                principalTable: "Marca",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupo_Linea_LineaId",
                table: "Grupo");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Grupo_GrupoId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Linea_LineaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marca_MarcaId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marca",
                table: "Marca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Linea",
                table: "Linea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grupo",
                table: "Grupo");

            migrationBuilder.RenameTable(
                name: "Marca",
                newName: "Marcas");

            migrationBuilder.RenameTable(
                name: "Linea",
                newName: "Lineas");

            migrationBuilder.RenameTable(
                name: "Grupo",
                newName: "Grupos");

            migrationBuilder.RenameIndex(
                name: "IX_Grupo_LineaId",
                table: "Grupos",
                newName: "IX_Grupos_LineaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marcas",
                table: "Marcas",
                column: "MarcaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lineas",
                table: "Lineas",
                column: "LineaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grupos",
                table: "Grupos",
                column: "GrupoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Lineas_LineaId",
                table: "Grupos",
                column: "LineaId",
                principalTable: "Lineas",
                principalColumn: "LineaId",
                onDelete: ReferentialAction.Restrict);

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
