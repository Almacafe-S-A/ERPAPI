using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Role_EstadoID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AspNetRoles",
                newName: "Estado");

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "AspNetRoles",
                newName: "Status");
        }
    }
}
