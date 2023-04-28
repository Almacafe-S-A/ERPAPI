using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class KardexValores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GoodsAuthorizationId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PdaNo",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Kardex",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SourceDocumentId",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceDocumentLine",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceDocumentName",
                table: "Kardex",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorMovimiento",
                table: "Kardex",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Kardex",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Kardex_GoodsAuthorizationId",
                table: "Kardex",
                column: "GoodsAuthorizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kardex_GoodsDeliveryAuthorization_GoodsAuthorizationId",
                table: "Kardex",
                column: "GoodsAuthorizationId",
                principalTable: "GoodsDeliveryAuthorization",
                principalColumn: "GoodsDeliveryAuthorizationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kardex_GoodsDeliveryAuthorization_GoodsAuthorizationId",
                table: "Kardex");

            migrationBuilder.DropIndex(
                name: "IX_Kardex_GoodsAuthorizationId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "GoodsAuthorizationId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "PdaNo",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SourceDocumentId",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SourceDocumentLine",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "SourceDocumentName",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "ValorMovimiento",
                table: "Kardex");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Kardex");
        }
    }
}
