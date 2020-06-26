using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FksProductReltonEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_IdEstado",
                table: "ProductRelation",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRelation_Estados_IdEstado",
                table: "ProductRelation",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRelation_Estados_IdEstado",
                table: "ProductRelation");

            migrationBuilder.DropIndex(
                name: "IX_ProductRelation_IdEstado",
                table: "ProductRelation");
        }
    }
}
