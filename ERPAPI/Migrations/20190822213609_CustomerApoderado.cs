using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerApoderado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocType",
                table: "ProductUserRelation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerTypeName",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Denominacion",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentidadApoderado",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreApoderado",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocType",
                table: "ProductUserRelation");

            migrationBuilder.DropColumn(
                name: "CustomerTypeName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Denominacion",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IdentidadApoderado",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NombreApoderado",
                table: "Customer");
        }
    }
}
