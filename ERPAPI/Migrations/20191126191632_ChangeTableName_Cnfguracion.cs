using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangeTableName_Cnfguracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GetConfiguracionesGenerales",
                table: "GetConfiguracionesGenerales");

            migrationBuilder.RenameTable(
                name: "GetConfiguracionesGenerales",
                newName: "ConfiguracionesGenerales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfiguracionesGenerales",
                table: "ConfiguracionesGenerales",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfiguracionesGenerales",
                table: "ConfiguracionesGenerales");

            migrationBuilder.RenameTable(
                name: "ConfiguracionesGenerales",
                newName: "GetConfiguracionesGenerales");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetConfiguracionesGenerales",
                table: "GetConfiguracionesGenerales",
                column: "Id");
        }
    }
}
