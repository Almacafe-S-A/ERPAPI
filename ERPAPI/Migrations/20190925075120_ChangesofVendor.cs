using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ChangesofVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyMin",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "QtyMin",
                table: "ConfigurationVendor");

            migrationBuilder.AddColumn<string>(
                name: "IdentityRepresentative",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RTNRepresentative",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepresentativeName",
                table: "Vendor",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryConfigurationLine_JournalEntryConfiguration_JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntryConfigurationLine_JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine");

            migrationBuilder.DropColumn(
                name: "IdentityRepresentative",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "RTNRepresentative",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "RepresentativeName",
                table: "Vendor");

            migrationBuilder.AddColumn<double>(
                name: "QtyMin",
                table: "Vendor",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "QtyMin",
                table: "ConfigurationVendor",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
