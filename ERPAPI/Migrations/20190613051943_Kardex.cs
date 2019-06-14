using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Kardex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormulasConcepto",
                columns: table => new
                {
                    IdformulaConcepto = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Idformula = table.Column<long>(nullable: true),
                    IdConcepto = table.Column<long>(nullable: true),
                    NombreConcepto = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    EstructuraConcepto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasConcepto", x => x.IdformulaConcepto);
                });

            migrationBuilder.CreateTable(
                name: "FormulasConFormulas",
                columns: table => new
                {
                    IdFormulaconformula = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdFormula = table.Column<long>(nullable: true),
                    IdFormulachild = table.Column<long>(nullable: true),
                    NombreFormulachild = table.Column<string>(nullable: true),
                    EstructuraConcepto = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    Fechamodificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasConFormulas", x => x.IdFormulaconformula);
                });

            migrationBuilder.CreateTable(
                name: "Incapacidades",
                columns: table => new
                {
                    Idincapacidad = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    DescripcionIncapacidad = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incapacidades", x => x.Idincapacidad);
                });

            migrationBuilder.CreateTable(
                name: "Kardex",
                columns: table => new
                {
                    KardexId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KardexDate = table.Column<DateTime>(nullable: false),
                    DocType = table.Column<long>(nullable: false),
                    DocName = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    TypeOperationId = table.Column<int>(nullable: false),
                    TypeOperationName = table.Column<string>(nullable: true),
                    DocumentId = table.Column<long>(nullable: false),
                    DocumentName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kardex", x => x.KardexId);
                });

            migrationBuilder.CreateTable(
                name: "KardexLine",
                columns: table => new
                {
                    KardexLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KardexId = table.Column<long>(nullable: false),
                    KardexDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    GoodsReceivedId = table.Column<long>(nullable: false),
                    ControlEstibaId = table.Column<long>(nullable: false),
                    ControlEstibaName = table.Column<string>(nullable: true),
                    ProducId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProducId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    QuantityEntry = table.Column<double>(nullable: false),
                    QuantityOut = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    TypeOperationId = table.Column<int>(nullable: false),
                    TypeOperationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KardexLine", x => x.KardexLineId);
                    table.ForeignKey(
                        name: "FK_KardexLine_Kardex_KardexId",
                        column: x => x.KardexId,
                        principalTable: "Kardex",
                        principalColumn: "KardexId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KardexLine_KardexId",
                table: "KardexLine",
                column: "KardexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormulasConcepto");

            migrationBuilder.DropTable(
                name: "FormulasConFormulas");

            migrationBuilder.DropTable(
                name: "Incapacidades");

            migrationBuilder.DropTable(
                name: "KardexLine");

            migrationBuilder.DropTable(
                name: "Kardex");
        }
    }
}
