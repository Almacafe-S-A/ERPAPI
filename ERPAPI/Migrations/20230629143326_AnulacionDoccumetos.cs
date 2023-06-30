using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AnulacionDoccumetos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Exento",
                table: "Invoice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CancelledDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdTipoDocumento = table.Column<long>(nullable: false),
                    TipoDocumento = table.Column<string>(nullable: true),
                    IdDocumento = table.Column<int>(nullable: false),
                    JournalEntryId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelledDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelledDocuments_TiposDocumento_IdTipoDocumento",
                        column: x => x.IdTipoDocumento,
                        principalTable: "TiposDocumento",
                        principalColumn: "IdTipoDocumento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CancelledDocuments_JournalEntry_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntry",
                        principalColumn: "JournalEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelledDocuments_IdTipoDocumento",
                table: "CancelledDocuments",
                column: "IdTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_CancelledDocuments_JournalEntryId",
                table: "CancelledDocuments",
                column: "JournalEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelledDocuments");

            migrationBuilder.DropColumn(
                name: "Exento",
                table: "Invoice");
        }
    }
}
