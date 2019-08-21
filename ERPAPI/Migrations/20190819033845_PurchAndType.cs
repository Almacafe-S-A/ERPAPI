using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PurchAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedCreated",
                table: "TypeAccount",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedCreated",
                table: "Purch",
                newName: "ModifiedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "TypeAccount",
                newName: "ModifiedCreated");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Purch",
                newName: "ModifiedCreated");
        }
    }
}
