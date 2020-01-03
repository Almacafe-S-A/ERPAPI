using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class UserRole_Estado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreDepartamento",
                table: "Departamento",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_NombreDepartamento",
                table: "Departamento",
                column: "NombreDepartamento",
                unique: true,
                filter: "[NombreDepartamento] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departamento_NombreDepartamento",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "AspNetUserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "NombreDepartamento",
                table: "Departamento",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
