using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DeleteAccountClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounting_AccountClass_AccountClassid",
                table: "Accounting");

            migrationBuilder.DropTable(
                name: "AccountClass");

            migrationBuilder.DropIndex(
                name: "IX_Accounting_AccountClassid",
                table: "Accounting");

            migrationBuilder.DropColumn(
                name: "AccountClassid",
                table: "Accounting");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountClassid",
                table: "Accounting",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountClass",
                columns: table => new
                {
                    AccountClassid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalBalance = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClass", x => x.AccountClassid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_AccountClassid",
                table: "Accounting",
                column: "AccountClassid");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounting_AccountClass_AccountClassid",
                table: "Accounting",
                column: "AccountClassid",
                principalTable: "AccountClass",
                principalColumn: "AccountClassid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
