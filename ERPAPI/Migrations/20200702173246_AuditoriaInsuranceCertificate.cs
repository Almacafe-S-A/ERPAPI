using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AuditoriaInsuranceCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "InsurancesCertificateLines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "InsurancesCertificateLines",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "InsurancesCertificateLines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "InsurancesCertificateLines",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "InsuranceCertificate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "InsuranceCertificate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "InsuranceCertificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "InsuranceCertificate",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "InsurancesCertificateLines");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "InsurancesCertificateLines");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "InsurancesCertificateLines");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "InsurancesCertificateLines");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "InsuranceCertificate");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "InsuranceCertificate");
        }
    }
}
