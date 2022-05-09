using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFieldsBOletadeSAlida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barco",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DireccionDestion",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuiRemisionNo",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrdenNo",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PNInglesas",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoBruto",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "Producto",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "QQInglesas",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QQPuerto",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tara",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TonPuerto",
                table: "BoletaDeSalida",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Transportista",
                table: "BoletaDeSalida",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoletaDeSalida_Producto",
                table: "BoletaDeSalida",
                column: "Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_BoletaDeSalida_Product_Producto",
                table: "BoletaDeSalida",
                column: "Producto",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoletaDeSalida_Product_Producto",
                table: "BoletaDeSalida");

            migrationBuilder.DropIndex(
                name: "IX_BoletaDeSalida_Producto",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "Barco",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "DireccionDestion",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "GuiRemisionNo",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "OrdenNo",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "PNInglesas",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "PesoBruto",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "Producto",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "QQInglesas",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "QQPuerto",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "Tara",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "TonPuerto",
                table: "BoletaDeSalida");

            migrationBuilder.DropColumn(
                name: "Transportista",
                table: "BoletaDeSalida");
        }
    }
}
