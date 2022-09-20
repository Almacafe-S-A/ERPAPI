using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedFieldAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "FixedAsset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "FixedAsset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "FixedAsset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serie",
                table: "FixedAsset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "FixedAsset");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "FixedAsset");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "FixedAsset");

            migrationBuilder.DropColumn(
                name: "Serie",
                table: "FixedAsset");
        }
    }
}
