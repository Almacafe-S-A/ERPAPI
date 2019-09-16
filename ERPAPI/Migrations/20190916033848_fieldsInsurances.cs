using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class fieldsInsurances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoInsurances",
                table: "Insurances",
                newName: "Path");

            migrationBuilder.AddColumn<string>(
                name: "DocumentName",
                table: "Insurances",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DocumentTypeId",
                table: "Insurances",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DocumentTypeName",
                table: "Insurances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentName",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "DocumentTypeName",
                table: "Insurances");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Insurances",
                newName: "PhotoInsurances");
        }
    }
}
