using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CustomerCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientConditions");

            migrationBuilder.CreateTable(
                name: "CustomerConditions",
                columns: table => new
                {
                    ClientConditionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConditionId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    IdTipoDocumento = table.Column<long>(nullable: false),
                    ClientConditionName = table.Column<string>(nullable: true),
                    LogicalCondition = table.Column<string>(nullable: true),
                    ValueToEvaluate = table.Column<string>(nullable: true),
                    ValueDecimal = table.Column<double>(nullable: false),
                    ValueString = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerConditions", x => x.ClientConditionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerConditions");

            migrationBuilder.CreateTable(
                name: "ClientConditions",
                columns: table => new
                {
                    ClientConditionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientConditionName = table.Column<string>(nullable: true),
                    ConditionId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    IdTipoDocumento = table.Column<long>(nullable: false),
                    LogicalCondition = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    ValueDecimal = table.Column<double>(nullable: false),
                    ValueString = table.Column<string>(nullable: true),
                    ValueToEvaluate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientConditions", x => x.ClientConditionId);
                });
        }
    }
}
