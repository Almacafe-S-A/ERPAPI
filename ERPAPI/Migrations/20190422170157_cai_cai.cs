using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class cai_cai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cai",
                table: "CAI",
                newName: "_cai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_cai",
                table: "CAI",
                newName: "cai");
        }
    }
}
