using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class EstadoPuesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Puesto",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Puesto",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Puesto");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Puesto");
        }
    }
}
