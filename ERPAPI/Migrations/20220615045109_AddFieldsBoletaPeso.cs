using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddFieldsBoletaPeso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clave_o",
                table: "Boleto_Sal");

            migrationBuilder.DropColumn(
                name: "observa_s",
                table: "Boleto_Sal");

            migrationBuilder.DropColumn(
                name: "Destino",
                table: "Boleto_Ent");

            migrationBuilder.RenameColumn(
                name: "nombre_oe",
                table: "Boleto_Ent",
                newName: "RTNTransportista");

            migrationBuilder.RenameColumn(
                name: "clave_o",
                table: "Boleto_Ent",
                newName: "DestinoI");

            migrationBuilder.AddColumn<decimal>(
                name: "PesoKGI",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoLBSI",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoQQI",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PesoTMI",
                table: "Boleto_Ent",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PesoKGI",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "PesoLBSI",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "PesoQQI",
                table: "Boleto_Ent");

            migrationBuilder.DropColumn(
                name: "PesoTMI",
                table: "Boleto_Ent");

            migrationBuilder.RenameColumn(
                name: "RTNTransportista",
                table: "Boleto_Ent",
                newName: "nombre_oe");

            migrationBuilder.RenameColumn(
                name: "DestinoI",
                table: "Boleto_Ent",
                newName: "clave_o");

            migrationBuilder.AddColumn<string>(
                name: "clave_o",
                table: "Boleto_Sal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "observa_s",
                table: "Boleto_Sal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destino",
                table: "Boleto_Ent",
                nullable: true);
        }
    }
}
