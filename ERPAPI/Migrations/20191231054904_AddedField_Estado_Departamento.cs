using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedField_Estado_Departamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Departamento",
                nullable: true);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Departamento");

            
        }
    }
}
