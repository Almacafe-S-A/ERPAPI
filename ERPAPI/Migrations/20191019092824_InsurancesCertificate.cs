using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InsurancesCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancesCertificate",
                columns: table => new
                {
                    InsurancesCertificateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsurancesId = table.Column<int>(nullable: false),
                    DateofInsurance = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    TotalInsurances = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotalLetras = table.Column<string>(nullable: true),
                    GrupoEconomicoId = table.Column<long>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    NoPoliza = table.Column<string>(nullable: true),
                    LugarFirma = table.Column<string>(nullable: true),
                    IdCertificated = table.Column<long>(nullable: false),
                    FechaFirma = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancesCertificate", x => x.InsurancesCertificateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancesCertificate");
        }
    }
}
