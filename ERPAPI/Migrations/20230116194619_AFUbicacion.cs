using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AFUbicacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FixedAssetDescription",
                table: "FixedAsset",
                newName: "Ubicacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "FixedAsset",
                newName: "FixedAssetDescription");
        }
    }
}
