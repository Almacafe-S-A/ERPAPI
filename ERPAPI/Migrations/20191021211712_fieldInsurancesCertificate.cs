using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class fieldInsurancesCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancesCertificateLine",
                columns: table => new
                {
                    InsurancesCertificateLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsurancesCertificateId = table.Column<int>(nullable: false),
                    TotalInsurancesLine = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotaldeductibleLine = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotalofProductLine = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotalInsurancesofProductLine = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DifferenceTotalofProductInsuranceLine = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotaldeductibleofProduct = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    TotalLetras = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    GrupoEconomicoId = table.Column<long>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    LugarFirma = table.Column<string>(nullable: true),
                    FechaFirma = table.Column<DateTime>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancesCertificateLine", x => x.InsurancesCertificateLineId);
                    table.ForeignKey(
                        name: "FK_InsurancesCertificateLine_InsurancesCertificate_InsurancesCertificateId",
                        column: x => x.InsurancesCertificateId,
                        principalTable: "InsurancesCertificate",
                        principalColumn: "InsurancesCertificateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsurancesCertificateLine_InsurancesCertificateId",
                table: "InsurancesCertificateLine",
                column: "InsurancesCertificateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancesCertificateLine");
        }
    }
}
