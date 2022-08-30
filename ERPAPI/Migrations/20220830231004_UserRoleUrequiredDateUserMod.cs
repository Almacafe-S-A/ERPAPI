using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class UserRoleUrequiredDateUserMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacion",
                table: "AspNetUserRoles",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UsuarioModificacion",
                table: "AspNetUserRoles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
