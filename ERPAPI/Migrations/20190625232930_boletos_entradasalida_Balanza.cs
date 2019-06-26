using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class boletos_entradasalida_Balanza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boleto_Ent",
                columns: table => new
                {
                    clave_e = table.Column<long>(nullable: false),
                    clave_C = table.Column<string>(nullable: true),
                    clave_o = table.Column<string>(nullable: true),
                    clave_p = table.Column<string>(nullable: true),
                    completo = table.Column<bool>(nullable: false),
                    fecha_e = table.Column<DateTime>(nullable: false),
                    hora_e = table.Column<string>(nullable: true),
                    placas = table.Column<string>(nullable: true),
                    conductor = table.Column<string>(nullable: true),
                    peso_e = table.Column<int>(nullable: false),
                    observa_e = table.Column<string>(nullable: true),
                    nombre_oe = table.Column<string>(nullable: true),
                    turno_oe = table.Column<string>(nullable: true),
                    unidad_e = table.Column<string>(nullable: true),
                    bascula_e = table.Column<string>(nullable: true),
                    t_entrada = table.Column<int>(nullable: false),
                    clave_u = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto_Ent", x => x.clave_e);
                });

            migrationBuilder.CreateTable(
                name: "Boleto_Sal",
                columns: table => new
                {
                    clave_e = table.Column<long>(nullable: false),
                    clave_o = table.Column<string>(nullable: true),
                    fecha_s = table.Column<DateTime>(nullable: false),
                    hora_s = table.Column<string>(nullable: true),
                    peso_s = table.Column<double>(nullable: false),
                    peso_n = table.Column<double>(nullable: false),
                    observa_s = table.Column<string>(nullable: true),
                    turno_s = table.Column<string>(nullable: true),
                    bascula_s = table.Column<string>(nullable: true),
                    s_manual = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto_Sal", x => x.clave_e);
                    table.ForeignKey(
                        name: "FK_Boleto_Sal_Boleto_Ent_clave_e",
                        column: x => x.clave_e,
                        principalTable: "Boleto_Ent",
                        principalColumn: "clave_e",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boleto_Sal");

            migrationBuilder.DropTable(
                name: "Boleto_Ent");
        }
    }
}
