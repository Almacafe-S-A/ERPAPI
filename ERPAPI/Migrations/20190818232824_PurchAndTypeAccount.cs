using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PurchAndTypeAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Dimensions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Dimensions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "AccountClasses",
                table: "Account",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Purch",
                columns: table => new
                {
                    PurchId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchCode = table.Column<string>(nullable: false),
                    PurchName = table.Column<string>(nullable: false),
                    RTN = table.Column<string>(nullable: false),
                    PurchTypeId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    taxGroup = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Identidad = table.Column<string>(nullable: false),
                    PhoneReferenceone = table.Column<string>(nullable: true),
                    CompanyReferenceone = table.Column<string>(nullable: false),
                    PhoneReferencetwo = table.Column<string>(nullable: true),
                    CompanyReferencetwo = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    GrupoEconomico = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purch", x => x.PurchId);
                });

            migrationBuilder.CreateTable(
                name: "TypeAccount",
                columns: table => new
                {
                    TypeAccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeAccountName = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAccount", x => x.TypeAccountId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purch");

            migrationBuilder.DropTable(
                name: "TypeAccount");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Dimensions");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Dimensions");

            migrationBuilder.DropColumn(
                name: "AccountClasses",
                table: "Account");
        }
    }
}
