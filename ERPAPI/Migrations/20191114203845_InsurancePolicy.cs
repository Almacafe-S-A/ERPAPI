using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class InsurancePolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancePolicy",
                columns: table => new
                {
                    InsurancePolicyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PolicyDate = table.Column<DateTime>(nullable: false),
                    PolicyDueDate = table.Column<DateTime>(nullable: false),
                    PolicyNumber = table.Column<string>(nullable: false),
                    InsurancesId = table.Column<int>(nullable: false),
                    InsurancesName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    LpsAmount = table.Column<double>(nullable: false),
                    DollarAmount = table.Column<double>(nullable: false),
                    AttachmentURL = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicy", x => x.InsurancePolicyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancePolicy");
        }
    }
}
