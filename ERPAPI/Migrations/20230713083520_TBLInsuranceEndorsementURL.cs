using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TBLInsuranceEndorsementURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachementFileName",
                table: "InsuranceEndorsement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentURL",
                table: "InsuranceEndorsement",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachementFileName",
                table: "InsuranceEndorsement");

            migrationBuilder.DropColumn(
                name: "AttachmentURL",
                table: "InsuranceEndorsement");
        }
    }
}
