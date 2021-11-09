using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TypeCOntractTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerContractType",
                table: "CustomerContractTerms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerContractTypeName",
                table: "CustomerContractTerms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerContractType",
                table: "CustomerContractTerms");

            migrationBuilder.DropColumn(
                name: "CustomerContractTypeName",
                table: "CustomerContractTerms");
        }
    }
}
