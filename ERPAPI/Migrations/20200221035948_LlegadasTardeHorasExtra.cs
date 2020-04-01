using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class LlegadasTardeHorasExtra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorasExtrasBiometrico",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBiometrico = table.Column<long>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    Horas = table.Column<int>(nullable: false),
                    Minutos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorasExtrasBiometrico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorasExtrasBiometrico_Biometricos_IdBiometrico",
                        column: x => x.IdBiometrico,
                        principalTable: "Biometricos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorasExtrasBiometrico_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LlegadasTardeBiometrico",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBiometrico = table.Column<long>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    Horas = table.Column<int>(nullable: false),
                    Minutos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlegadasTardeBiometrico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LlegadasTardeBiometrico_Biometricos_IdBiometrico",
                        column: x => x.IdBiometrico,
                        principalTable: "Biometricos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LlegadasTardeBiometrico_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorasExtrasBiometrico_IdBiometrico",
                table: "HorasExtrasBiometrico",
                column: "IdBiometrico");

            migrationBuilder.CreateIndex(
                name: "IX_HorasExtrasBiometrico_IdEmpleado",
                table: "HorasExtrasBiometrico",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_LlegadasTardeBiometrico_IdBiometrico",
                table: "LlegadasTardeBiometrico",
                column: "IdBiometrico");

            migrationBuilder.CreateIndex(
                name: "IX_LlegadasTardeBiometrico_IdEmpleado",
                table: "LlegadasTardeBiometrico",
                column: "IdEmpleado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorasExtrasBiometrico");

            migrationBuilder.DropTable(
                name: "LlegadasTardeBiometrico");
        }
    }
}
