using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Added_field_Estado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "InsuranceEndorsement",
                nullable: false,
                defaultValue: 0L);           

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEndorsement_EstadoId",
                table: "InsuranceEndorsement",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceEndorsement_Estados_EstadoId",
                table: "InsuranceEndorsement",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceEndorsement_Estados_EstadoId",
                table: "InsuranceEndorsement");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceEndorsement_EstadoId",
                table: "InsuranceEndorsement");

           

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "InsuranceEndorsement");

           
        }
    }
}
