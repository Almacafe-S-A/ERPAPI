using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class BolertaPeso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacion",
                table: "UnitOfMeasure",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioCreacion",
                table: "UnitOfMeasure",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorPergamino",
                table: "InventarioBodegaHabilitada",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "InventarioBodegaHabilitada",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "FactorPergamino",
                table: "InventarioBodegaHabilitada",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Factor",
                table: "InventarioBodegaHabilitada",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "InventarioBodegaHabilitada",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Boleto_Ent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Boleto_Ent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoKG",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoLBS",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoQQ",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoTM",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "SubProductId",
                table: "Boleto_Ent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubProductName",
                table: "Boleto_Ent",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boleto_Ent_CustomerId",
                table: "Boleto_Ent",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Boleto_Ent_SubProductId",
                table: "Boleto_Ent",
                column: "SubProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boleto_Ent_Customer_CustomerId",
                table: "Boleto_Ent",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Boleto_Ent_SubProduct_SubProductId",
                table: "Boleto_Ent",
                column: "SubProductId",
                principalTable: "SubProduct",
                principalColumn: "SubproductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boleto_Ent_Customer_CustomerId",
                table: "Boleto_Ent");

            migrationBuilder.DropForeignKey(
                name: "FK_Boleto_Ent_SubProduct_SubProductId",
                table: "Boleto_Ent");

            migrationBuilder.DropIndex(
                name: "IX_Boleto_Ent_CustomerId",
                table: "Boleto_Ent");

            migrationBuilder.DropIndex(
                name: "IX_Boleto_Ent_SubProductId",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "PesoKG",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "PesoLBS",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "PesoQQ",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "PesoTM",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "SubProductId",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "SubProductName",
                table: "Boleto_Ent");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacion",
                table: "UnitOfMeasure",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioCreacion",
                table: "UnitOfMeasure",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorPergamino",
                table: "InventarioBodegaHabilitada",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "InventarioBodegaHabilitada",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "FactorPergamino",
                table: "InventarioBodegaHabilitada",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Factor",
                table: "InventarioBodegaHabilitada",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "InventarioBodegaHabilitada",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");
        }
    }
}
