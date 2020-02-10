using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class PhoneLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneLines",
                columns: table => new
                {
                    PhoneLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: false),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    IdBranch = table.Column<int>(nullable: false),
                    CompanyPhone = table.Column<string>(nullable: true),
                    DueBalanceLps = table.Column<double>(nullable: false),
                    DueBalanceUS = table.Column<double>(nullable: false),
                    PaymentLps = table.Column<double>(nullable: false),
                    PaymentUS = table.Column<double>(nullable: false),
                    TotalLps = table.Column<double>(nullable: false),
                    TotalUS = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneLines", x => x.PhoneLineId);
                    table.ForeignKey(
                        name: "FK_PhoneLines_Branch_IdBranch",
                        column: x => x.IdBranch,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneLines_IdBranch",
                table: "PhoneLines",
                column: "IdBranch");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneLines");
        }
    }
}
