using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Biometricos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biometricos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biometricos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biometricos_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesBiometricos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBiometrico = table.Column<long>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesBiometricos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesBiometricos_Biometricos_IdBiometrico",
                        column: x => x.IdBiometrico,
                        principalTable: "Biometricos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesBiometricos_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biometricos_IdEstado",
                table: "Biometricos",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesBiometricos_IdBiometrico",
                table: "DetallesBiometricos",
                column: "IdBiometrico");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesBiometricos_IdEmpleado",
                table: "DetallesBiometricos",
                column: "IdEmpleado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesBiometricos");

            migrationBuilder.DropTable(
                name: "Biometricos");
        }
    }
}
