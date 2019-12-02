using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CamposAdicionalesCuentas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name:"Totaliza", 
                nullable:false,
                defaultValue:false,
                table: "Accounting"
            );

            migrationBuilder.AddColumn<string>(
                name: "DeudoraAcreedora",
                nullable: false,
                defaultValue: "D",
                table: "Accounting"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Totaliza", table: "Accounting");
            migrationBuilder.DropColumn("DeudoraAcreedora", table: "Accounting");
        }
    }
}
