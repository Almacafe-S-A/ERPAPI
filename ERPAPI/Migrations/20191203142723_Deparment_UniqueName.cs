using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Deparment_UniqueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "NombreDepartamento",
            //    table: "Departamento",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_NombreDepartamentos",
                table: "Departamento",
                column: "NombreDepartamento",
                unique: true,
                filter: "[NombreDepartamento] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departamento_NombreDepartamentos",
                table: "Departamento");

            //migrationBuilder.AlterColumn<string>(
            //    name: "NombreDepartamento",
            //    table: "Departamento",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldNullable: true);
        }
    }
}
