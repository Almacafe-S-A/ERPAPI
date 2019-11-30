using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CamposAdicionalesCuentas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name:"Mayoriza", 
                nullable:false,
                defaultValue:false,
                table: "Accounting"
            );

            migrationBuilder.AddColumn<string>(
                name: "NaturalezaCuenta",
                nullable: false,
                defaultValue: "D",
                table: "Accounting"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Mayoriza", table: "Accounting");
            migrationBuilder.DropColumn("NaturalezaCuenta", table: "Accounting");
        }
    }
}
