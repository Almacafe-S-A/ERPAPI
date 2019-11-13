using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Policy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PolicyRoles_IdPolicy",
                table: "PolicyRoles",
                column: "IdPolicy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRoles_IdRol",
                table: "PolicyRoles",
                column: "IdRol");

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyRoles_Policy_IdPolicy",
                table: "PolicyRoles",
                column: "IdPolicy",
                principalTable: "Policy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyRoles_AspNetRoles_IdRol",
                table: "PolicyRoles",
                column: "IdRol",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolicyRoles_Policy_IdPolicy",
                table: "PolicyRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyRoles_AspNetRoles_IdRol",
                table: "PolicyRoles");

            migrationBuilder.DropIndex(
                name: "IX_PolicyRoles_IdPolicy",
                table: "PolicyRoles");

            migrationBuilder.DropIndex(
                name: "IX_PolicyRoles_IdRol",
                table: "PolicyRoles");
        }
    }
}
