using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class JournalEntryandJournalEntryLineFieldsModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioModificacion",
                table: "JournalEntryLine",
                newName: "ModifiedUser");

            migrationBuilder.RenameColumn(
                name: "UsuarioCreacion",
                table: "JournalEntryLine",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "FechaModificacion",
                table: "JournalEntryLine",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "JournalEntryLine",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedUser",
                table: "JournalEntryLine",
                newName: "UsuarioModificacion");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "JournalEntryLine",
                newName: "FechaModificacion");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JournalEntryLine",
                newName: "UsuarioCreacion");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "JournalEntryLine",
                newName: "FechaCreacion");
        }
    }
}
