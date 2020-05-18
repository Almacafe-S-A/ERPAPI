using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddedPartytoCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "CheckAccountLines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyTypeId",
                table: "CheckAccountLines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "CheckAccountLines");

            migrationBuilder.DropColumn(
                name: "PartyTypeId",
                table: "CheckAccountLines");
        }
    }
}
