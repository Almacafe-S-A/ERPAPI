using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ActivoAsegurado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuredAssets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsurancePolicyId = table.Column<long>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: true),
                    AssetName = table.Column<string>(nullable: true),
                    AssetDeductible = table.Column<decimal>(nullable: false),
                    AssetInsuredValue = table.Column<decimal>(nullable: false),
                    MerchadiseTotalValue = table.Column<decimal>(nullable: false),
                    MerchandiseInsuredValue = table.Column<decimal>(nullable: false),
                    InsuredDiference = table.Column<decimal>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuredAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuredAssets_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuredAssets_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuredAssets_InsurancePolicy_InsurancePolicyId",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicy",
                        principalColumn: "InsurancePolicyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuredAssets_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuredAssets_BranchId",
                table: "InsuredAssets",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuredAssets_EstadoId",
                table: "InsuredAssets",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuredAssets_InsurancePolicyId",
                table: "InsuredAssets",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuredAssets_WarehouseId",
                table: "InsuredAssets",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuredAssets");
        }
    }
}
