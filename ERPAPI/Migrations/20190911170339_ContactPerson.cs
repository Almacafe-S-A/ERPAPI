using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ContactPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactPerson",
                columns: table => new
                {
                    ContactPersonId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactPersonName = table.Column<string>(nullable: true),
                    VendorId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    ContactPersonIdentity = table.Column<string>(nullable: true),
                    ContactPersonPhone = table.Column<string>(nullable: true),
                    ContactPersonCityId = table.Column<int>(nullable: false),
                    ContactPersonCity = table.Column<string>(nullable: true),
                    ContactPersonEmail = table.Column<string>(nullable: true),
                    ContactPersonIdEstado = table.Column<long>(nullable: false),
                    ContactPersonEstado = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPerson", x => x.ContactPersonId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPerson");
        }
    }
}
