using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FixedAsset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepreciationFixedAsset",
                columns: table => new
                {
                    DepreciationFixedAssetId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FixedAssetId = table.Column<long>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    January = table.Column<double>(nullable: false),
                    February = table.Column<double>(nullable: false),
                    March = table.Column<double>(nullable: false),
                    April = table.Column<double>(nullable: false),
                    May = table.Column<double>(nullable: false),
                    June = table.Column<double>(nullable: false),
                    July = table.Column<double>(nullable: false),
                    August = table.Column<double>(nullable: false),
                    September = table.Column<double>(nullable: false),
                    October = table.Column<double>(nullable: false),
                    November = table.Column<double>(nullable: false),
                    December = table.Column<double>(nullable: false),
                    TotalDepreciated = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepreciationFixedAsset", x => x.DepreciationFixedAssetId);
                });

            migrationBuilder.CreateTable(
                name: "FixedAsset",
                columns: table => new
                {
                    FixedAssetId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FixedAssetName = table.Column<string>(nullable: true),
                    FixedAssetDescription = table.Column<string>(nullable: true),
                    AssetDate = table.Column<DateTime>(nullable: false),
                    FixedAssetGroupId = table.Column<long>(nullable: false),
                    FixedAssetCode = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    CenterCostId = table.Column<long>(nullable: false),
                    CenterCostName = table.Column<string>(nullable: true),
                    FixedActiveLife = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    ResidualValue = table.Column<double>(nullable: false),
                    ToDepreciate = table.Column<double>(nullable: false),
                    TotalDepreciated = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedAsset", x => x.FixedAssetId);
                });

            migrationBuilder.CreateTable(
                name: "FixedAssetGroup",
                columns: table => new
                {
                    FixedAssetGroupId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FixedAssetGroupName = table.Column<string>(nullable: true),
                    FixedAssetGroupDescription = table.Column<string>(nullable: true),
                    FixedGroupCode = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FixedAssetGroupId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedAssetGroup", x => x.FixedAssetGroupId);
                    table.ForeignKey(
                        name: "FK_FixedAssetGroup_FixedAssetGroup_FixedAssetGroupId1",
                        column: x => x.FixedAssetGroupId1,
                        principalTable: "FixedAssetGroup",
                        principalColumn: "FixedAssetGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssetGroup_FixedAssetGroupId1",
                table: "FixedAssetGroup",
                column: "FixedAssetGroupId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepreciationFixedAsset");

            migrationBuilder.DropTable(
                name: "FixedAsset");

            migrationBuilder.DropTable(
                name: "FixedAssetGroup");
        }
    }
}
