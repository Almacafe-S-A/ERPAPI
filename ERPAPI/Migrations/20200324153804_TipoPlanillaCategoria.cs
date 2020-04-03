using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TipoPlanillaCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoriaId",
                table: "TipoPlanillas",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_TipoPlanillas_CategoriaId",
                table: "TipoPlanillas",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPlanillas_CategoriasPlanillas_CategoriaId",
                table: "TipoPlanillas",
                column: "CategoriaId",
                principalTable: "CategoriasPlanillas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPlanillas_CategoriasPlanillas_CategoriaId",
                table: "TipoPlanillas");

            migrationBuilder.DropIndex(
                name: "IX_TipoPlanillas_CategoriaId",
                table: "TipoPlanillas");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "TipoPlanillas");
        }
    }
}
