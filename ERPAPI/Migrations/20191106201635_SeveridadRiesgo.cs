using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class SeveridadRiesgo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeveridadRiesgo",
                columns: table => new
                {
                    IdSeveridad = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Impacto = table.Column<long>(nullable: false),
                    Probabilidad = table.Column<long>(nullable: false),
                    LimiteCalidadInferior = table.Column<long>(nullable: false),
                    LimeteCalidadSuperir = table.Column<long>(nullable: false),
                    RangoInferiorSeveridad = table.Column<double>(nullable: false),
                    RangoSuperiorSeveridad = table.Column<double>(nullable: false),
                    Nivel = table.Column<string>(nullable: true),
                    ColorHexadecimal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeveridadRiesgo", x => x.IdSeveridad);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeveridadRiesgo");
        }
    }
}
