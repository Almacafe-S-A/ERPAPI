using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ForeignsKeysEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Employees",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "IdBank",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "IdBranch",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "IdCity",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdCountry",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "IdCurrency",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "IdDepartamento",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdEscala",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdEstado",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdPuesto",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdState",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdTipoContrato",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdBank",
                table: "Employees",
                column: "IdBank");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdBranch",
                table: "Employees",
                column: "IdBranch");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCity",
                table: "Employees",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCountry",
                table: "Employees",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCurrency",
                table: "Employees",
                column: "IdCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdDepartamento",
                table: "Employees",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdEscala",
                table: "Employees",
                column: "IdEscala");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdEstado",
                table: "Employees",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdPuesto",
                table: "Employees",
                column: "IdPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdState",
                table: "Employees",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdTipoContrato",
                table: "Employees",
                column: "IdTipoContrato");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Bank_IdBank",
                table: "Employees",
                column: "IdBank",
                principalTable: "Bank",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Branch_IdBranch",
                table: "Employees",
                column: "IdBranch",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_City_IdCity",
                table: "Employees",
                column: "IdCity",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Country_IdCountry",
                table: "Employees",
                column: "IdCountry",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Currency_IdCurrency",
                table: "Employees",
                column: "IdCurrency",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departamento_IdDepartamento",
                table: "Employees",
                column: "IdDepartamento",
                principalTable: "Departamento",
                principalColumn: "IdDepartamento",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Escala_IdEscala",
                table: "Employees",
                column: "IdEscala",
                principalTable: "Escala",
                principalColumn: "IdEscala",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Estados_IdEstado",
                table: "Employees",
                column: "IdEstado",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Puesto_IdPuesto",
                table: "Employees",
                column: "IdPuesto",
                principalTable: "Puesto",
                principalColumn: "IdPuesto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_State_IdState",
                table: "Employees",
                column: "IdState",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TipoContrato_IdTipoContrato",
                table: "Employees",
                column: "IdTipoContrato",
                principalTable: "TipoContrato",
                principalColumn: "IdTipoContrato",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Bank_IdBank",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Branch_IdBranch",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_City_IdCity",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Country_IdCountry",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Currency_IdCurrency",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departamento_IdDepartamento",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Escala_IdEscala",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Estados_IdEstado",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Puesto_IdPuesto",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_State_IdState",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TipoContrato_IdTipoContrato",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdBank",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdBranch",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCity",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCountry",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdCurrency",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdDepartamento",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdEscala",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdEstado",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdPuesto",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdState",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdTipoContrato",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdBank",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdBranch",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdCity",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdCountry",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdCurrency",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdDepartamento",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdEscala",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdPuesto",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdState",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdTipoContrato",
                table: "Employees");
        }
    }
}
