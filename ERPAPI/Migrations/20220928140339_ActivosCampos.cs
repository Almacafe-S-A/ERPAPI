using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ActivosCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DepreciacionMensualNIIF",
                table: "FixedAsset",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalaDepreciarNIIF",
                table: "FixedAsset",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VidaUtilNIIF",
                table: "FixedAsset",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepreciacionMensualNIIF",
                table: "FixedAsset");

            migrationBuilder.DropColumn(
                name: "TotalaDepreciarNIIF",
                table: "FixedAsset");

            migrationBuilder.DropColumn(
                name: "VidaUtilNIIF",
                table: "FixedAsset");
        }
    }
}
