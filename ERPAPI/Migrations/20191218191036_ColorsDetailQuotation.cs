using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ColorsDetailQuotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColorsDetailQuotation",
                columns: table => new
                {
                    ColorsDetailQuotationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorId = table.Column<long>(nullable: false),
                    QuotationCode = table.Column<long>(nullable: false),
                    QuotationVersion = table.Column<long>(nullable: false),
                    QuotationDetailId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorsDetailQuotation", x => x.ColorsDetailQuotationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationDetail_QuotationCode",
                table: "QuotationDetail",
                column: "QuotationCode");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationDetail_Quotation_QuotationCode",
                table: "QuotationDetail",
                column: "QuotationCode",
                principalTable: "Quotation",
                principalColumn: "QuotationCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationDetail_Quotation_QuotationCode",
                table: "QuotationDetail");

            migrationBuilder.DropTable(
                name: "ColorsDetailQuotation");

            migrationBuilder.DropIndex(
                name: "IX_QuotationDetail_QuotationCode",
                table: "QuotationDetail");
        }
    }
}
