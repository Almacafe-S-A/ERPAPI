using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EstadoSubproducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "SubProduct",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "SubProduct",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "SubProduct");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "SubProduct");
        }
    }
}
